using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETCore_Training.Data;
using ASP.NETCore_Training.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ASP.NETCore_Training.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IHostingEnvironment _he;
        public ProductController(ApplicationDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            var data = _db.Products.Include(c=>c.ProductTypes).Include(c=>c.SpecialTag).ToList();
            return View(data);
        }

        //Get Create method
        public ActionResult Create()
        {
            //Create dropdown
            ViewData["productTypeId"]=new SelectList(_db.ProductTypes.ToList(),"Id","ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products products, IFormFile image)
        {
            if (image!=null)
            {
                var name = Path.Combine(_he.WebRootPath + "/images", Path.GetFileName(image.FileName));
                await image.CopyToAsync(new FileStream(name, FileMode.Create));
                products.Image = "images/"+image.FileName;
            }

            if (image==null)
            {
                products.Image = "images/noimage.PNG";
            }

            if(ModelState.IsValid){
                _db.Products.Add(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }


    }
}
