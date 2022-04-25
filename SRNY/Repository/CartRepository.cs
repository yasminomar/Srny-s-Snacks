using SRNY.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRNY.Repository
{
    public class CartRepository : ICartRepository
    {
        SRNYContext context;
        public CartRepository(SRNYContext _context)
        {
            this.context = _context;
        }
        public List<Cart> GetAll()
        {
            return context.Cart.ToList();

        }
        public Cart GetByID(int id)
        {
            return context.Cart.FirstOrDefault(s => s.Id == id);

        }
        public List<Cart> GetAllCartsByUserID(string id)
        {
            return context.Cart.Where(s => s.ApplicationUser.Id == id).ToList();

        }
        public Cart GetLastCartByUserID(string id)
        {
            return context.Cart.Where(s => s.ApplicationUser.Id == id).ToList().LastOrDefault();

        }
        public void Insert(Cart Cart)
        {
            context.Cart.Add(Cart);
            context.SaveChanges();

        }
        public void Update(int id, Cart Cart)
        {
            Cart old = GetByID(id);
            old.ApplicationUser.Id = Cart.ApplicationUser.Id;
            context.SaveChanges();

        }
        public void Delete(int id)
        {
            context.Cart.Remove(GetByID(id));
            context.SaveChanges();

        }
        public ApplicationUser GetUserByCartId(int cartId, string userId)
        {
            return GetLastCartByUserID(userId).ApplicationUser;

        }
    }
}
