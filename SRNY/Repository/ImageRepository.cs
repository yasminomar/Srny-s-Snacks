using SRNY.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRNY.Repository
{
    public class ImageRepository : IImageRepository
    {
        SRNYContext context;
        public ImageRepository(SRNYContext _context)
        {
            this.context = _context;
        }
        public List<Images> GetAll()
        {
            return context.Images.ToList();

        }
        public Images GetByID(int id)
        {
            return context.Images.FirstOrDefault(s => s.Id == id);

        }
        public List<Images> GetByProductID(int id)
        {
            return context.Images.Where(s => s.ProductId == id).ToList();

        }
        public void Insert(Images Images)
        {
            context.Images.Add(Images);
            context.SaveChanges();

        }
        public void Update(int id, Images Images)
        {
            Images old = GetByID(id);
            old.image = Images.image;
            old.ProductId = Images.ProductId;
            context.SaveChanges();

        }
        public void Delete(int id)
        {
            context.Images.Remove(GetByID(id));
            context.SaveChanges();

        }
        public List<Images> GetByProductId(int id)
        {
            return context.Images.Where(I => I.ProductId == id).ToList();

        }
        public Images GetMainImage(int id)
        {
            return context.Images.FirstOrDefault(I => I.ProductId == id);

        }


    }
}
