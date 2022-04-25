using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SRNY.Models;
using SRNY.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SRNY.Controllers
{
    public class HomeController : Controller
    {
        IProductRepository ProductRepository;
        IImageRepository ImageRepository;
        ISizeRepository SizeRepository;
        ICategoriesInProductRepository CategoriesInProductRepository;
        ICategoryRepository CategoryRepository;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepo, IImageRepository imageRepo, ISizeRepository sizeRepo, ICategoriesInProductRepository categoriesInProductRepo, ICategoryRepository categoryRepo)
        {
            this.ProductRepository = productRepo;
            this.ImageRepository = imageRepo;
            this.SizeRepository = sizeRepo;
            this.CategoriesInProductRepository = categoriesInProductRepo;
            this.CategoryRepository = categoryRepo;

            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Images> imageList = ImageRepository.GetAll();
            List<Size> sizeList = SizeRepository.GetAll();
            List<CategoriesInProduct> categoriesList = CategoriesInProductRepository.GetAll();
            List<Category> categoreisNamesList = CategoryRepository.GetAll();
            ViewData["imgs"] = imageList;
            ViewData["size"] = sizeList;
            ViewData["categories"] = categoriesList;
            ViewData["categoriesName"] = categoreisNamesList;
            return View(GetProductList(1));
            //return View();
        }
        [HttpPost]
        public IActionResult Index(int currentPageIndex)
        {
            List<Images> imageList = ImageRepository.GetAll();
            List<Size> sizeList = SizeRepository.GetAll();
            List<CategoriesInProduct> categoriesList = CategoriesInProductRepository.GetAll();
            List<Category> categoriesNamesList = CategoryRepository.GetAll();
            ViewData["imgs"] = imageList;
            ViewData["size"] = sizeList;
            ViewData["categories"] = categoriesList;
            ViewData["categoriesName"] = categoriesNamesList;
            return View(GetProductList(currentPageIndex));

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

        public IActionResult GetImage(int id)
        {
            Images LastImage = ImageRepository.GetByProductID(id).LastOrDefault();
            return Json(LastImage);
        }
        private ProductViewModel GetProductList(int currentPage)
        {
            int maxRowsPerPage = 6;
            ProductViewModel productModel = new ProductViewModel();
            productModel.ProductList = (from product in ProductRepository.GetAll() select product)
                .OrderBy(x => x.Id)
                .Skip((currentPage - 1) * maxRowsPerPage)
                .Take(maxRowsPerPage).ToList();
            double pageCount = (double)((decimal)ProductRepository.GetAll().Count() / Convert.ToDecimal(maxRowsPerPage));
            productModel.pageCount = (int)Math.Ceiling(pageCount);
            productModel.currentPageIndex = currentPage;
            return productModel;
        }
    }
}