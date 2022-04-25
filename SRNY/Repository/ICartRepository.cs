using SRNY.Models;
using System.Collections.Generic;

namespace SRNY.Repository
{
    public interface ICartRepository
    {
        void Delete(int id);
        List<Cart> GetAll();
        List<Cart> GetAllCartsByUserID(string id);
        Cart GetByID(int id);
        Cart GetLastCartByUserID(string id);
        ApplicationUser GetUserByCartId(int cartId, string userId);
        void Insert(Cart Cart);
        void Update(int id, Cart Cart);
    }
}