using ASP.NETCore_Training.Data;
using ASP.NETCore_Training.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ASP.NETCore_Training.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        ApplicationDbContext _db;
        public UserController(Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    TempData["save"] = "User has been created successfully";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();          
        }
    }
}
