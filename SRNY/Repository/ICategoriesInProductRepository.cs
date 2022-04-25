using SRNY.Models;
using System.Collections.Generic;

namespace SRNY.Repository
{
    public interface ICategoriesInProductRepository
    {
        void Delete(int id);
        List<CategoriesInProduct> GetAll();
        List<CategoriesInProduct> GetByCategoryId(int id);
        CategoriesInProduct GetByID(int id);
        List<CategoriesInProduct> GetByProductID(int id);
        List<Category> GetCategoriesName(int id);
        void Insert(CategoriesInProduct CategoriesInProduct);
        void Update(int id, CategoriesInProduct CategoriesInProduct);
        void DeleteCategories(List<CategoriesInProduct> prop);

    }
}