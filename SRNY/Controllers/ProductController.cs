using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SRNY.Models;
using SRNY.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SRNY.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository ProductRepository;
        IImageRepository ImageRepository;
        ISizeRepository SizeRepository;
        ICategoriesInProductRepository CategoriesInProductRepository;
        ICategoryRepository CategoryRepository;
        ICartRepository CartRepository;
        IProductInCartRepository ProductInCartRepository;
        private readonly UserManager<ApplicationUser> userMannger;

        public ProductController(IProductRepository productRepo, IImageRepository imageRepo, ISizeRepository sizeRepo, ICategoriesInProductRepository categoriesInProductRepo, ICategoryRepository categoryRepo, ICartRepository cartRepo, UserManager<ApplicationUser> userMannger, IProductInCartRepository productInCartRepo)
        {
            this.ProductRepository = productRepo;
            this.ImageRepository = imageRepo;
            this.SizeRepository = sizeRepo;
            this.CategoriesInProductRepository = categoriesInProductRepo;
            this.CategoryRepository = categoryRepo;
            this.CartRepository= cartRepo;
            this.userMannger = userMannger;
            this.ProductInCartRepository = productInCartRepo;
        }
        public IActionResult Index()
        {
            List<Images> imageList = ImageRepository.GetAll();
            List<Size> sizeList = SizeRepository.GetAll();
            List<CategoriesInProduct> categoryList = CategoriesInProductRepository.GetAll();
            List<Category> categoriesNamesList = CategoryRepository.GetAll();
            List<Cart> cartList = CartRepository.GetAll();
            string userid = userMannger.GetUserId(HttpContext.User);
            if (userid != null)
            {
                Cart cart = CartRepository.GetLastCartByUserID(userid);
                int cartID = cart.Id;
                ViewData["cartID"] = cartID;
            }
            ViewData["imgs"] = imageList;
            ViewData["size"] = sizeList;
            ViewData["categories"] = categoryList;
            ViewData["categoriesName"] = categoriesNamesList;
            ViewData["carts"] = cartList;
            ViewData["userid"] = userid;
            return View(GetProductList(1));
        }

        [HttpPost]
        public IActionResult Index(int currentPageIndex)
        {
            List<Images> imageList = ImageRepository.GetAll();
            List<Size> sizeList = SizeRepository.GetAll();
            List<CategoriesInProduct> categureList = CategoriesInProductRepository.GetAll();
            List<Category> categureNamesList = CategoryRepository.GetAll();
            List<Cart> cartList = CartRepository.GetAll();
            string userid = userMannger.GetUserId(HttpContext.User);
            if (userid != null)
            {
                Cart cart = CartRepository.GetLastCartByUserID(userid);
                int cartID = cart.Id;
                ViewData["cartID"] = cartID;
            }
            ViewData["imgs"] = imageList;
            ViewData["size"] = sizeList;
            ViewData["categories"] = categureList;
            ViewData["categoriesName"] = categureNamesList;
            ViewData["carts"] = cartList;
            ViewData["userid"] = userid;
            return View(GetProductList(currentPageIndex));
        }

        public IActionResult GetImage(int id)
        {
            Images LastImage = ImageRepository.GetByProductID(id).LastOrDefault();
            return Json(LastImage);
        }

        private ProductViewModel GetProductList(int currentPage)
        {
            int maxRowsPerPage = 6;
            ProductViewModel productModel = new ProductViewModel();
            productModel.ProductList = (from product in ProductRepository.GetAll()select product)
                .OrderBy(x=>x.Id)
                .Skip((currentPage-1)*maxRowsPerPage)
                .Take(maxRowsPerPage).ToList();
            double pageCount=(double)((decimal)ProductRepository.GetAll().Count()/Convert.ToDecimal(maxRowsPerPage));
            productModel.pageCount=(int)Math.Ceiling(pageCount);
            productModel.currentPageIndex=currentPage;
            return productModel;
        } 
        [HttpPost]
        public IActionResult AddToCart(int id,string size,int quantity, string actionName)
        {
            string userid = userMannger.GetUserId(HttpContext.User);
            if(userid == null)
            {
                return RedirectToAction("Login","Account");
            }
            else
            {
                Cart cart = CartRepository.GetLastCartByUserID(userid);
                ProductInCart product = new ProductInCart();
                product.ProductId = id;
                product.CartId = cart.Id;
                product.Quantity = quantity;
                product.Size= size;
                ProductInCartRepository.Insert(product);
                if(actionName == "Index")
                {
                    return RedirectToAction("Index","Product");
                }
                return RedirectToAction("ProductDetails", new { id = id, status="success" });
            }
        }


        public IActionResult ProductDetails(int id, string status)
        {
            Product product = ProductRepository.GetByID(id);
            List<Images> ImageList = ImageRepository.GetByProductId(id);
            ViewBag.Images = ImageList;
            Images mainImage = ImageRepository.GetMainImage(id);
            ViewBag.mainImage = mainImage;
            List<Category> CategoryList = CategoriesInProductRepository.GetCategoriesName(id);
            ViewBag.CategoryList = CategoryList;
            List<Size> SizeList = SizeRepository.GetByProductId(id);
            ViewBag.SizeList = SizeList;
            ViewBag.Status = status;
            return View(product);
        }

        //public IActionResult ProductDetails(int id,string status)
        //{
        //    Product product = ProductRepository.GetByID(id);
        //    List<Images> ImageList = ImageRepository.GetByProductId(id);
        //    ViewBag.Images = ImageList;
        //    Images mainImage = ImageRepository.GetMainImage(id);
        //    ViewBag.mainImage = mainImage;
        //    List<Category> CategoryList = CategoriesInProductRepository.GetCategoriesName(id);
        //    ViewBag.CategoryList = CategoryList;
        //    List<Size> SizeList = SizeRepository.GetByProductId(id);
        //    ViewBag.SizeList = SizeList;
        //    ViewBag.Status = status;
        //    return View(product);
        //}

        public IActionResult Quantity(int id)
        {
            Size size = SizeRepository.GetByID(id);
            return Json(size);
        }

        public IActionResult QuickView(int id)
        {
            string userid = userMannger.GetUserId(HttpContext.User);
            Product product = ProductRepository.GetByID(id);
            ViewBag.Product = product;
            List<Images> ImageList = ImageRepository.GetByProductId(id);
            ViewBag.Images = ImageList;
            Images mainImage = ImageRepository.GetMainImage(id);
            ViewBag.mainImage = mainImage;
            List<Category> CategoryList = CategoriesInProductRepository.GetCategoriesName(id);
            ViewBag.CategoryList = CategoryList;
            List<Size> SizeList = SizeRepository.GetByProductId(id);
            ViewData["userid"] = userid;
            ViewBag.SizeList = SizeList;
            return PartialView("_ProductDetails");
        }
        private ProductViewModel GetProductsInCategory(int CategoryId, int currentPage)
        {
            int maxRowsPerPage = 6;
            ProductViewModel productModel = new ProductViewModel();
            productModel.ProductList = (from product in CategoryRepository.GetProductByCategoryId(CategoryId) select product)
                .OrderBy(x => x.Id)
                .Skip((currentPage - 1) * maxRowsPerPage)
                .Take(maxRowsPerPage).ToList();
            double pageCount = (double)((decimal)CategoryRepository.GetProductByCategoryId(CategoryId).Count() / Convert.ToDecimal(maxRowsPerPage));
            productModel.pageCount = (int)Math.Ceiling(pageCount);
            productModel.currentPageIndex = currentPage;
            return productModel;
        }
        public IActionResult Category(int id)
        {
            List<CategoriesInProduct> categoryList = CategoriesInProductRepository.GetByCategoryId(id);
            List<Images> imageList = ImageRepository.GetAll();
            List<Size> sizeList = SizeRepository.GetAll();
            Category categoryName = CategoryRepository.GetByID(id);
            List<Category> categoriesName = CategoryRepository.GetAll();
            ViewData["imgs"] = imageList;
            ViewData["size"] = sizeList;
            ViewData["categories"] = categoryList;
            ViewData["categoriesName"] = categoryName;
            ViewData["categoriesNameList"] = categoriesName;
            return View(GetProductsInCategory(id, 1));
        }

    }
}
