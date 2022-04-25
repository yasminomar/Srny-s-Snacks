using SRNY.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRNY.Repository
{
    public class ProductRepository : IProductRepository
    {
        SRNYContext context;
        public ProductRepository(SRNYContext _context)
        {
            this.context = _context;
        }
        public List<Product> GetAll()
        {
            return context.Product.ToList();

        }
        public Product GetByID(int id)
        {
            return context.Product.FirstOrDefault(s => s.Id == id);

        }
        public void Insert(Product Product)
        {
            context.Product.Add(Product);
            context.SaveChanges();

        }
        public void Update(int id, Product Product)
        {
            Product old = GetByID(id);
            old.Name = Product.Name;
            old.Description = Product.Description;
            old.Date = Product.Date;
            old.SubName=Product.SubName;
            context.SaveChanges();

        }
        public void Delete(int id)
        {
            context.Product.Remove(GetByID(id));
            context.SaveChanges();

        }

        public int countProduct()
        {
            return context.Product.Count();
        }
    }
}
