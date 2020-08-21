using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASP.NETCore_Training.Models;
using ASP.NETCore_Training.Data;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCore_Training.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var data = _db.Products.Include(p => p.ProductTypes).Include(s => s.SpecialTag).ToList();
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
