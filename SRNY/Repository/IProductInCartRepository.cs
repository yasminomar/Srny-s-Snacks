using SRNY.Models;
using System.Collections.Generic;

namespace SRNY.Repository
{
    public interface IProductInCartRepository
    {
        void Delete(int id);
        List<ProductInCart> GetAll();
        List<ProductInCart> GetByCartID(int id);
        ProductInCart GetByID(int id);
        List<ProductInCart> GetByProductID(int id);
        void Insert(ProductInCart ProductInCart);
        void Update(int id, ProductInCart ProductInCart);
    }
}