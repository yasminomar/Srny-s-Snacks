using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SRNY.Models;
using SRNY.Repository;
using SRNY.ViewModel;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SRNY.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userMannger;
        private readonly SignInManager<ApplicationUser> signInManager;
        ICartRepository CartRepository;
        IOrderRepository OrdersRepository;



        public AccountController(IOrderRepository orderRepo, UserManager<ApplicationUser> userMannger, SignInManager<ApplicationUser> signInManager, ICartRepository cartRepo)
        {
            this.userMannger = userMannger;
            this.signInManager = signInManager;
            this.CartRepository = cartRepo;
            this.OrdersRepository = orderRepo;

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerUser)
        {
            if (!ModelState.IsValid)
            {
                return View(registerUser);
            }
            try
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = registerUser.UserName;
                applicationUser.Email = registerUser.Email;
                applicationUser.PasswordHash = registerUser.Password;
                applicationUser.Street = registerUser.Street;
                applicationUser.City = registerUser.City;
                applicationUser.ZipCode = registerUser.ZipCode;

                IdentityResult result = await userMannger.CreateAsync(applicationUser, registerUser.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(applicationUser, false);
                    Cart Cart= new Cart();
                    Cart.ApplicationUser = applicationUser;
                    CartRepository.Insert(Cart);
                    Order order = new Order();
                    order.ApplicationUser=applicationUser;
                    order.Status = "Initial";
                    order.FirstName = registerUser.UserName;
                    order.LastName = "empty";
                    order.Apartment = "empty";
                    order.Street= registerUser.Street;
                    order.Email= registerUser.Email;
                    order.Phone = "empty";
                    OrdersRepository.Add(order);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(registerUser);
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return View(loginViewModel);
            }
            ApplicationUser user = await userMannger.FindByEmailAsync(loginViewModel.Email);
            if (user != null)
            {
                bool result = await userMannger.CheckPasswordAsync(user, loginViewModel.Password);
                if (result)
                {

                    //List<Claim> claims = new List<Claim>();

                    //await signInManager.SignInWithClaimsAsync(user, loginViewModel.RememberMe, claims);
                    await signInManager.SignInAsync(user, loginViewModel.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Password is incorrect");
            return View(loginViewModel);

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public IActionResult RegisterAdmin()
        {
            return View("RegisterAdminAsync");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> RegisterAdminAsync(RegisterViewModel registerViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            try
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = registerViewModel.UserName;
                applicationUser.Email = registerViewModel.Email;
                applicationUser.PasswordHash = registerViewModel.Password;
                applicationUser.Street = registerViewModel.Street;
                applicationUser.City = registerViewModel.City;
                applicationUser.ZipCode = registerViewModel.ZipCode;

                IdentityResult result = await userMannger.CreateAsync(applicationUser, registerViewModel.Password);
                if (result.Succeeded)
                {
                    await userMannger.AddToRoleAsync(applicationUser, "Admin");
                    await signInManager.SignInAsync(applicationUser, false);
                    return RedirectToAction("Users", "Admin");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View("RegisterAdminAsync",registerViewModel);
        }
    }
}