using SRNY.Models;
using System.Collections.Generic;

namespace SRNY.Repository
{
    public interface IProductRepository
    {
        void Delete(int id);
        List<Product> GetAll();
        Product GetByID(int id);
        void Insert(Product Product);
        void Update(int id, Product Product);
        int countProduct();

    }
}