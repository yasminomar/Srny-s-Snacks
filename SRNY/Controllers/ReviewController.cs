using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SRNY.Models;
using SRNY.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRNY.Controllers
{
    public class ReviewController : Controller
    {

        private readonly UserManager<ApplicationUser> userMannger;
        IReviewRepository ReviewRepository;
        IProductRepository ProductRepository;
        IImageRepository ImageRepository;
        public ReviewController(IReviewRepository ReviewRepo,IImageRepository imageRepo, IProductRepository productRepo, UserManager<ApplicationUser> userMannger)
        {
            this.ReviewRepository = ReviewRepo;
            this.ImageRepository = imageRepo;
            this.ProductRepository = productRepo;
            this.userMannger = userMannger;
        }
        [HttpGet]
        public IActionResult Index()
        {

            List<Images> ImagesList =ImageRepository.GetAll();
            ViewData["ImagesList"] = ImagesList;

            List<Reviews> ReviewList = ReviewRepository.GetAll();
            ViewData["Reviews"] = ReviewList;
            List<Product> Productlist = ProductRepository.GetAll();
            List<ApplicationUser> userList = userMannger.Users.ToList();
            ViewData["userList"] = userList;
            string username = HttpContext.User.Identity.Name;
            ViewData["user"] = username;
            ViewData["prod"] = Productlist;

            return View();
        }
        [HttpGet]
        public IActionResult Review()
        {
            
            string currentUserId = userMannger.GetUserId(HttpContext.User);
           


            if (currentUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            List<Product> Productlist = ProductRepository.GetAll();
            string username = HttpContext.User.Identity.Name;
            ViewData["user"] = username;
            ViewData["prod"] = Productlist;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Review(Reviews rev)
        {
            ApplicationUser applicationUser = await userMannger.GetUserAsync(User);
            string currentUserId = userMannger.GetUserId(HttpContext.User);
            string userEmail = applicationUser?.Email;
            ViewData["userEmail"] = userEmail;

            if (rev.Body != null && rev.ProductId != 0)
            {
                rev.ApplicationUser= applicationUser;
                ReviewRepository.Add(rev);
                return RedirectToAction("Index");
            }
            List<Product> Productlist = ProductRepository.GetAll();
            ViewData["prod"] = Productlist;
            return View(rev);
        }


    }
}