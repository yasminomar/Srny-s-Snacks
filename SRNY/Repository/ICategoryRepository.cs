using SRNY.Models;
using System.Collections.Generic;

namespace SRNY.Repository
{
    public interface ICategoryRepository
    {
        void Delete(int id);
        List<Category> GetAll();
        Category GetByID(int id);
        List<Product> GetProductByCategoryId(int id);
        void Insert(Category Category);
        void Update(int id, Category Category);
        int countCategories();

    }
}