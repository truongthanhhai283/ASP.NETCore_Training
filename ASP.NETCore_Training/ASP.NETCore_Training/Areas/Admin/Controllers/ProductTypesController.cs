using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NETCore_Training.Data;
using ASP.NETCore_Training.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCore_Training.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _db;
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var data = _db.ProductTypes.ToList();
            //return View(_db.ProductTypes.ToList()); ;
            return View(data);
        }

        //Create GET action Method
        public ActionResult Create()
        {
            return View();
        }

        //Create POST action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        //Edit GET action Method
        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType==null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //Edit POST action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        //Details GET action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //Details POST action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
            return RedirectToAction(nameof(Index));            
        }
    }
}
