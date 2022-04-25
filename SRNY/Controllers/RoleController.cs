using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SRNY.ViewModel;
using System.Threading.Tasks;

namespace SRNY.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return View(roleViewModel);
            }
            IdentityRole role = new IdentityRole(roleViewModel.RoleName);
            IdentityResult result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Users","Admin");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }

            return View(roleViewModel);
        }
    }
}