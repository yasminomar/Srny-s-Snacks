using SRNY.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRNY.Repository
{
    public class SizeRepository : ISizeRepository
    {
        SRNYContext context;
        public SizeRepository(SRNYContext _context)
        {
            this.context = _context;
        }
        public List<Size> GetAll()
        {
            return context.Size.ToList();

        }
        public Size GetByID(int id)
        {
            return context.Size.FirstOrDefault(s => s.Id == id);

        }
        public List<Size> GetByProductID(int id)
        {
            return context.Size.Where(s => s.ProductId == id).ToList();

        }
        public void Insert(Size Size)
        {
            context.Size.Add(Size);
            context.SaveChanges();

        }
        public void Update(int id, Size Size)
        {
            Size old = GetByID(id);
            old.Price = Size.Price;
            old.ProductId = Size.ProductId;
            old.Available = Size.Available;
            old.size = Size.size;
            context.SaveChanges();

        }
        public void Delete(int id)
        {
            context.Size.Remove(GetByID(id));
            context.SaveChanges();

        }
        public List<Size> GetByProductId(int id)
        {
            return context.Size.Where(P => P.ProductId == id).ToList();

        }

    }
}
