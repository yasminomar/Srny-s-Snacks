using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SRNY.Models;
using SRNY.Repository;
using System.Collections.Generic;

namespace SRNY.Controllers
{
    public class CheckOutController : Controller
    {
        IProductInCartRepository ProductInCartRepository;
        IOrderRepository OrdersRepository;
        ISizeRepository SizeRepository;
        ICartRepository CartRepository;
        private readonly UserManager<ApplicationUser> userMannger;
        IProductRepository ProductRepository;



        public CheckOutController(IProductRepository productRepo, IOrderRepository ordersRepo, UserManager<ApplicationUser> userMannger, IProductInCartRepository productInCartRepo, ISizeRepository sizeRepo, ICartRepository cartRepo)
        {
            this.OrdersRepository = ordersRepo;
            this.ProductInCartRepository = productInCartRepo;
            this.SizeRepository = sizeRepo;
            this.CartRepository= cartRepo;
            this.userMannger = userMannger;
            this.ProductRepository = productRepo;

        }

        public IActionResult Order()
        {
            string userid = userMannger.GetUserId(HttpContext.User);
            Cart cart = CartRepository.GetLastCartByUserID(userid);
            int cartID = cart.Id;
            List<Size> sizeList = SizeRepository.GetAll();
            List<ProductInCart> ProductInCartList = ProductInCartRepository.GetByCartID(cartID);
            Order Order = OrdersRepository.GetByUserId(userid);
            List<Product> productList = new List<Product> { };
            foreach (var item in ProductInCartList)
            {
                Product product=ProductRepository.GetByID(item.ProductId);
                productList.Add(product);
            }

            ViewData["size"] = sizeList;
            ViewData["productInCart"] = ProductInCartList;
            ViewData["order"] = Order;
            ViewData["products"] = productList;


            return View();
        }
        [HttpPost]
        public IActionResult Order(int id,Order order)
        {
            if (order.Phone != null && order.Email != null && order.Street != null && order.Apartment != null && order.FirstName != null && order.LastName != null)
            {
                string userid = userMannger.GetUserId(HttpContext.User);
                OrdersRepository.Update(id, order);
                return RedirectToAction("Index","Home");
            }
            return View("Order", order);

        }
    }
}
