using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SRNY.Models;
using SRNY.Repository;
using SRNY.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SRNY.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        IProductRepository ProductRepository;
        IImageRepository ImageRepository;
        ISizeRepository SizeRepository;
        ICategoriesInProductRepository CateguresInProductRepository;
        ICategoryRepository CateguryRepository;
        private UserManager<ApplicationUser> userMannger;
        IWebHostEnvironment webHostEnvironment;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IReviewRepository reviewRepo;
        private readonly ICartRepository cartRepo;
        private readonly IOrderRepository orderRepository;
        private readonly IProductInCartRepository productInCartRepo;

        public AdminController(
            IProductRepository productRepo,
            RoleManager<IdentityRole> roleManager,
            IWebHostEnvironment webHostEnvironment , 
            UserManager<ApplicationUser> userMannger,
            IImageRepository imageRepo,
            ISizeRepository sizeRepo,
            ICategoriesInProductRepository categuresInProductRepo,
            IOrderRepository orderRepository,
            ICartRepository cartRepo,
            IReviewRepository reviewRepo,
            IProductInCartRepository productInCartRepo,
            ICategoryRepository categureRepo)
        {
            this.ProductRepository = productRepo;
            this.ImageRepository = imageRepo;
            this.SizeRepository = sizeRepo;
            this.CateguresInProductRepository = categuresInProductRepo;
            this.CateguryRepository = categureRepo;
            this.userMannger = userMannger;
            this.roleManager = roleManager;
            this.productInCartRepo = productInCartRepo;
            this.webHostEnvironment = webHostEnvironment;
            this.reviewRepo = reviewRepo;
            this.cartRepo= cartRepo;
            this.orderRepository= orderRepository;
        }
        public IActionResult Index()
        {
            ViewData["countProdcut"] = ProductRepository.countProduct();
            ViewData["countCategories"] = CateguryRepository.countCategories();
            ViewData["countUsers"] = userMannger.Users.ToList().Count();
            ViewData["countOrder"] = orderRepository.countOrder();
            return View();
        }
        public IActionResult Product()
        {
            ViewData["ImageList"] = ImageRepository.GetAll(); 
            ViewData["SizeList"] = SizeRepository.GetAll(); 
            ViewData["categoryProductList"] = CateguresInProductRepository.GetAll();
            ViewData["categoryList"] = CateguryRepository.GetAll();
            List<Product> productList = ProductRepository.GetAll();
            return View("Product",productList);
        }

        [HttpGet]
        public IActionResult NewProduct()
        {
            ViewData["Categories"] = CateguryRepository.GetAll();
           return View();
        }

        [HttpPost]
        public IActionResult NewProduct(Product prd)
        {
           // var all = prd.Categories;
            CategoriesInProduct obj = new CategoriesInProduct();
            obj.ProductId = prd.Id;
            //obj.CategoryId = prd.Categories.Id;
            CateguresInProductRepository.Insert(obj);
            if (ModelState.IsValid == true)
            {
                ProductRepository.Insert(prd);
                return RedirectToAction("Product");
            }
            return View("NewProduct", prd);
        }



        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            Product prd = ProductRepository.GetByID(id);
            ViewData["SizeSelect"] = SizeRepository.GetByProductID(id);
            ViewData["ImageSelect"] = ImageRepository.GetByProductID(id);
            ViewData["categoryProductList"] = CateguresInProductRepository.GetAll();
            ViewData["categoryList"] = CateguryRepository.GetAll();
            return View(prd);
        }
        [HttpPost]
        public IActionResult EditProduct(int id , Product product)
        {
            ViewData["SizeSelect"] = SizeRepository.GetByProductID(id);
            ViewData["categoryProductList"] = CateguresInProductRepository.GetAll();
            ViewData["categoryList"] = CateguryRepository.GetAll();
            List<Size>sizeList = product.Sizes;
            List<Images> imagesList = product.Images;
            if (ModelState.IsValid == true)
            {
                ProductRepository.Update(id,product);
                foreach (Size item in sizeList)
                {
                    SizeRepository.Update(item.Id, item);
                }

                return RedirectToAction("Product");
            }
            return View("EditProduct", product);
        }


        public IActionResult AddCategoryInProduct(int ProductId)
        {
            List<Category> Categories = CateguryRepository.GetAll();
           // List<CategoriesInProduct> categoriesInProducts = CateguresInProductRepository.GetAll();
           // List<Category> categories = new List<Category>();  
           // //foreach (var Category in Categories)
           //// {
           //     foreach (var item in categoriesInProducts)
           //     {
           //         if(item.ProductId == ProductId)
           //         {
           //             var cat = new Category();
           //         //cat.Name = Category.Name;
           //         cat.Id = item.CategoryId;
           //             categories.Add(cat);
           //         }
           //     }
           // //}
            ViewData["Categories"] = Categories;
            return View();
        }
        #region Size OF Product
        [HttpGet]
        public IActionResult AddSize(int ProductId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSize(Size sz)
        {
            if (ModelState.IsValid == true)
            {
                SizeRepository.Insert(sz);
                Product prop = ProductRepository.GetByID(sz.ProductId);
                return RedirectToAction("EditProduct", prop);
            }
            return View("AddSize", sz);
        }
        public IActionResult DeleteSize(int Id, int ProductId)
        {
            SizeRepository.Delete(Id);
            Product prop = ProductRepository.GetByID(ProductId);
            return RedirectToAction("EditProduct", prop);
        }
        #endregion
        #region Image Of Product
        public IActionResult uploadImage(int ProductId)
        {
            return View();
        }
        [HttpPost]
        public IActionResult uploadImage(IFormFile Image ,Images img )
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "assets/images");

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Image.CopyTo(fileStream);
                fileStream.Close();
            }
            if (ModelState.IsValid == true)
            {
                img.image = uniqueFileName;
                ImageRepository.Insert(img);
                Product prop = ProductRepository.GetByID(img.ProductId);
                return RedirectToAction("EditProduct", prop);
            }
            return View("uploadImage", img);

        }
        public IActionResult DeleteImage(int Id,int ProductId) {

            ImageRepository.Delete(Id);
            Product prop = ProductRepository.GetByID(ProductId);
            return RedirectToAction("EditProduct",prop);
        }

        #endregion
        #region Delete
        public IActionResult DeleteProduct(int Id)
        {
            List<Reviews> reviews = reviewRepo.GetByProductrId(Id);
            foreach (Reviews review in reviews)
            {
                reviewRepo.Delete(review.Id);
            }

            List<ProductInCart> productInCarts = productInCartRepo.GetByProductID(Id);
            foreach (ProductInCart product in productInCarts)
            {
                productInCartRepo.Delete(product.Id);
            }

            List<Images> images = ImageRepository.GetByProductID(Id);
            foreach (Images item in images)
            {
                ImageRepository.Delete(item.Id);
            }

            List<Size> sizes = SizeRepository.GetByProductID(Id);
            foreach (Size size in sizes)
            {
                SizeRepository.Delete(size.Id);
            }

            List<CategoriesInProduct> categoriesInProducts = CateguresInProductRepository.GetByProductID(Id);
            ProductRepository.Delete(Id);
            return RedirectToAction("Product" ,"Admin");
        }
        #endregion

        #region Details
        public IActionResult GetOne(int id)
        {
            Product prd = ProductRepository.GetByID(id);
            ViewData["Image"] = ImageRepository.GetByProductID(id).FirstOrDefault();
            ViewData["Sizes"] = SizeRepository.GetByProductID(id);
            ViewData["categoryProductList"] = CateguresInProductRepository.GetAll();
            ViewData["categoryList"] = CateguryRepository.GetAll();
            return View("GetOneProduct",prd);
        }
        #endregion


        #region Categories
        public IActionResult Categories()
        {
            List<Category> catgortList = CateguryRepository.GetAll();
            return View("Categories",catgortList);
        }

        #region Add Category
        [HttpGet]
        public IActionResult NewCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewCategory(Category categure)
        {
            if (ModelState.IsValid == true)
            {
                CateguryRepository.Insert(categure);
                return RedirectToAction("Categories");
            }
            return View("NewCategory", categure);
        }
        #endregion
        #region Edit Category
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            Category categure = CateguryRepository.GetByID(id);
            return View(categure);
        }
        [HttpPost]
        public IActionResult EditCategory(int id, Category categure)
        {
            if (ModelState.IsValid == true)
            {
                CateguryRepository.Update(id, categure);
                return RedirectToAction("Categories");
            }
            return View("EditCategory", categure);
        }
        #endregion
        #region Delete Category
        public IActionResult DeleteCategory(int id)
        {
            List<CategoriesInProduct> ctInProp = CateguresInProductRepository.GetByCategoryId(id);
            CateguresInProductRepository.DeleteCategories(ctInProp);
            CateguryRepository.Delete(id);
            return RedirectToAction("Categories");
        }
        #endregion
        #endregion

        #region Order
        public IActionResult Orders()
        {
            List<Order> ordertList = orderRepository.GetAll();
            return View("Orders" , ordertList);
        }
        [HttpGet]
        public IActionResult EditOrder(int Id)
        {
            Order order = orderRepository.GetById(Id);
            return View(order);
        }
        [HttpPost]
        public IActionResult EditOrder(int Id ,Order order)
        {
            orderRepository.Update(Id, order);
            return RedirectToAction("Orders", "Admin");
        }
        public IActionResult DeleteOrder(int Id)
        {
            orderRepository.Delete(Id);
            return RedirectToAction("Orders", "Admin");
        }

        #endregion

        #region User
        public async Task<IActionResult> Users()
        {
            List<ApplicationUser> userList = userMannger.Users.ToList();
            return View("Users",userList);
        }
        #region Add User
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
      [HttpPost]
      public async Task<IActionResult> AddUser(RegisterViewModel registerUser)
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
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
                else
                {
                    
                    return RedirectToAction("Users", "Admin");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View("AddUser",registerUser);
        }
        #endregion
        #region Edit User
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userMannger.FindByIdAsync(id);
            //var rolesName = await userMannger.GetRolesAsync(user);
            //List<UserRoles> userRoles = new List<UserRoles>();
            //foreach (var item in rolesName)
            //{
            //    UserRoles userRoles1 = new UserRoles();
            //    userRoles1.userId = user.Id;
            //    userRoles1.roleName = item;
            //    userRoles.Add(userRoles1);

            //}
            //ViewData["Roles"] = userRoles;
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(string id, ApplicationUser user)
        {
            var editUserModel = await userMannger.FindByIdAsync(id);
            if (ModelState.IsValid == true)
            {
                if (user != null)
                {
                    editUserModel.UserName = user.UserName;
                    editUserModel.Email = user.Email;
                    editUserModel.City = user.City;
                    editUserModel.Street = user.Street;
                    editUserModel.ZipCode = user.ZipCode;
                    var result = await userMannger.UpdateAsync(editUserModel);
                    return RedirectToAction("Users");

                }
            }
            return View("EditUser", user);
        }
        #endregion
        #region Delete User
        
        public async Task<ActionResult> DeleteUser(string Id)
        {

            ApplicationUser UserToDelete = await userMannger.FindByIdAsync(Id);
            List<Reviews> reviews = reviewRepo.GetByUserId(Id);
            foreach (Reviews review in reviews)
            {
                reviewRepo.Delete(review.Id);
            }
            List<Cart> carts = cartRepo.GetAllCartsByUserID(Id);
            foreach (Cart cart in carts)
            {
                List<ProductInCart> productInCarts = productInCartRepo.GetByCartID(cart.Id);
                foreach (ProductInCart product in productInCarts)
                {
                    productInCartRepo.Delete(product.Id);
                }
                cartRepo.Delete(cart.Id);
            }
            List<Order> orders = orderRepository.GetAllByUserId(Id);
            foreach (Order order in orders)
            {
                orderRepository.Delete(order.Id);
            }

            if (UserToDelete != null)
            {
                IdentityResult result = await userMannger.DeleteAsync(UserToDelete);
                if (result.Succeeded)
                {
                    return RedirectToAction("Users", "Admin");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }

            return RedirectToAction("User","Admin");
        }
        #endregion

        #endregion
    }
}
