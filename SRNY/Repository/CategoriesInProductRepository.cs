using SRNY.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRNY.Repository
{
    public class CategoriesInProductRepository : ICategoriesInProductRepository
    {
        SRNYContext context;
        public CategoriesInProductRepository(SRNYContext _context)
        {
            this.context = _context;
        }
        public List<CategoriesInProduct> GetAll()
        {
            return context.CategoriesInProduct.ToList();

        }
        public CategoriesInProduct GetByID(int id)
        {
            return context.CategoriesInProduct.FirstOrDefault(s => s.Id == id);

        }
        public List<CategoriesInProduct> GetByProductID(int id)
        {
            return context.CategoriesInProduct.Where(s => s.ProductId == id).ToList();

        }
        public void Insert(CategoriesInProduct CategoriesInProduct)
        {
            context.CategoriesInProduct.Add(CategoriesInProduct);
            context.SaveChanges();

        }
        public void Update(int id, CategoriesInProduct CategoriesInProduct)
        {
            CategoriesInProduct old = GetByID(id);
            old.ProductId = CategoriesInProduct.ProductId;
            old.CategoryId = CategoriesInProduct.CategoryId;
            context.SaveChanges();

        }
        public void Delete(int id)
        {
            context.CategoriesInProduct.Remove(GetByID(id));
            context.SaveChanges();

        }
        public List<CategoriesInProduct> GetByCategoryId(int id)
        {
            return context.CategoriesInProduct.Where(C => C.CategoryId == id).ToList();

        }

        public List<Category> GetCategoriesName(int id)
        {
            List<CategoriesInProduct> categories = GetByProductID(id);
            List<Category> categoriesName = new List<Category>();
            foreach (var Item in categories)
            {
                Category category = context.Category.SingleOrDefault(C => C.Id == Item.CategoryId);
                categoriesName.Add(new Category()
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }
            return categoriesName;
        }
        public void DeleteCategories(List<CategoriesInProduct> prop)
        {
            foreach (var categure in prop)
            {
                context.CategoriesInProduct.Remove(categure);
            }
            context.SaveChanges();

        }


    }
}
