using SRNY.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRNY.Repository
{
    public class OrderRepository : IOrderRepository
    {
        SRNYContext context;
        public OrderRepository(SRNYContext _context)
        {
            this.context = _context;
        }
        public List<Order> GetAll()
        {
            return context.Order.ToList();
        }
        public Order GetById(int id)
        {
            return context.Order.FirstOrDefault(x => x.Id == id);
        }
 
        public void Add(Order order)
        {
            context.Order.Add(order);
            context.SaveChanges();

        }
        public void Delete(int id)
        {
            context.Order.Remove(GetById(id));
            context.SaveChanges();

        }
        public Order GetOrderByCartId(int cartId)
        {
            return context.Order.FirstOrDefault(x => x.CartId == cartId);
        }
        public List<Order> GetAllByUserId(string Id)
        {
            return context.Order.Where(s => s.ApplicationUser.Id == Id).ToList();
        }
        public Order GetByUserId(string id)
        {
            return context.Order.Where(s => s.ApplicationUser.Id == id).ToList().LastOrDefault();

        }
        public int countOrder()
        {
            return context.Order.Count();

        }
        public void FirstUpdate(int id, Order order)
        {
            Order old = GetById(id);
            old.ShipId = order.ShipId;
            old.CartId = order.CartId;
            old.Status = order.Status;
            context.SaveChanges();

        }
        public void Update(int id, Order order)
        {
            Order old = GetById(id);
            old.Phone = order.Phone;
            old.Email = order.Email;
            old.Street = order.Street;
            old.FirstName = order.FirstName;
            old.LastName = order.LastName;
            old.Apartment = order.Apartment;
            old.zip = order.zip;
            old.Status = order.Status;
            old.CartId = order.CartId;
            old.ShipId= order.ShipId;
            context.SaveChanges();
        }
    }
}
