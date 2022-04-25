using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SRNY.Models;
using SRNY.Repository;
using System.Collections.Generic;

namespace SRNY.Controllers
{
    public class CartController : Controller
    {
        IProductRepository ProductRepository;
        IImageRepository ImageRepository;
        ISizeRepository SizeRepository;
        ICartRepository CartRepository;
        IShippingRepository ShippingRepository;
        IProductInCartRepository ProductInCartRepository;
        IOrderRepository OrdersRepository;
        private readonly UserManager<ApplicationUser> userMannger;

        public CartController(IProductRepository productRepo, IImageRepository imageRepo, ISizeRepository sizeRepo, UserManager<ApplicationUser> userMannger, ICartRepository cartRepo , IProductInCartRepository productInCartRepo , IShippingRepository shippingRepo, IOrderRepository orderRepo)
        {
            this.ProductRepository = productRepo;
            this.ImageRepository = imageRepo;
            this.SizeRepository = sizeRepo;
            this.userMannger = userMannger;
            this.CartRepository = cartRepo;
            this.ProductInCartRepository = productInCartRepo;
            this.ShippingRepository = shippingRepo;
            this.OrdersRepository = orderRepo;
        }
        public IActionResult Index()
        {
            List<Images> imageList = ImageRepository.GetAll();
            List<Product> productList = ProductRepository.GetAll();
            string userid = userMannger.GetUserId(HttpContext.User);
            Cart cart = CartRepository.GetLastCartByUserID(userid);
            int cartID = cart.Id;
            List<Size> sizeList = SizeRepository.GetAll();
            List<ProductInCart> ProductInCartList = ProductInCartRepository.GetByCartID(cartID);
            List<Shipping> ShippingList= ShippingRepository.GetAll();

            ViewData["imgs"] = imageList;
            ViewData["size"] = sizeList;
            ViewData["Products"] = productList;
            ViewData["userid"] = userid;
            ViewData["ProductInCartList"] = ProductInCartList;
            ViewData["ShippingList"] = ShippingList;

            return View();
            
        }
        public IActionResult RemoveProduct(int id)
        {
                ProductInCartRepository.Delete(id);
                return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult CreateOrder(int ShipId)
        {
            string userid = userMannger.GetUserId(HttpContext.User);
            Cart cart = CartRepository.GetLastCartByUserID(userid);
            int cartID = cart.Id;
            Order order = OrdersRepository.GetByUserId(userid);
            order.ShipId = ShipId;
            order.CartId = cartID;
            order.Status = "sent";
            OrdersRepository.FirstUpdate(order.Id, order);
            return RedirectToAction("Order","CheckOut");
        }


    }
}
