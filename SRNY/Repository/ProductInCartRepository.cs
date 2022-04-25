using SRNY.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRNY.Repository
{
    public class ProductInCartRepository : IProductInCartRepository
    {
        SRNYContext context;
        public ProductInCartRepository(SRNYContext _context)
        {
            this.context = _context;
        }
        public List<ProductInCart> GetAll()
        {
            return context.ProductInCart.ToList();

        }
        public ProductInCart GetByID(int id)
        {
            return context.ProductInCart.FirstOrDefault(s => s.Id == id);

        }
        public List<ProductInCart> GetByProductID(int id)
        {
            return context.ProductInCart.Where(s => s.ProductId == id).ToList();

        }
        
        public List<ProductInCart> GetByCartID(int id)
        {
            return context.ProductInCart.Where(s => s.CartId == id).ToList();

        }
        public void Insert(ProductInCart ProductInCart)
        {
            context.ProductInCart.Add(ProductInCart);
            context.SaveChanges();

        }
        public void Update(int id, ProductInCart ProductInCart)
        {
            ProductInCart old = GetByID(id);
            old.CartId = ProductInCart.CartId;
            old.ProductId = ProductInCart.ProductId;
            context.SaveChanges();

        }
        public void Delete(int id)
        {
            context.ProductInCart.Remove(GetByID(id));
            context.SaveChanges();

        }
      
    }
}
