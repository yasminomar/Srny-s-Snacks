using SRNY.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRNY.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        SRNYContext context;
        public CategoryRepository(SRNYContext _context)
        {
            this.context = _context;
        }
        public List<Category> GetAll()
        {
            return context.Category.ToList();

        }
        public Category GetByID(int id)
        {
            return context.Category.FirstOrDefault(s => s.Id == id);

        }

        public void Insert(Category Category)
        {
            context.Category.Add(Category);
            context.SaveChanges();

        }
        public void Update(int id, Category Category)
        {
            Category old = GetByID(id);
            old.Name = Category.Name;
            context.SaveChanges();

        }
        public void Delete(int id)
        {
            context.Category.Remove(GetByID(id));
            context.SaveChanges();

        }
        public List<Product> GetProductByCategoryId(int id)
        {
            List<CategoriesInProduct> categoriesInProduct = context.CategoriesInProduct.Where(c => c.CategoryId == id).ToList();
            List<Product> products = new List<Product>();
            foreach (var Item in categoriesInProduct)
            {
                Product product = context.Product.FirstOrDefault(P => P.Id == Item.ProductId);
                products.Add(product);
            }
            return products;
        }

        public int countCategories()
        {
            return context.Category.Count();
        }

    }
}
