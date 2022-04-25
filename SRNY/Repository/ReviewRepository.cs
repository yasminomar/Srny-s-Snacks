using SRNY.Models;
using System.Collections.Generic;
using System.Linq;


namespace SRNY.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        SRNYContext context;
        public ReviewRepository(SRNYContext _context)
        {
            this.context = _context;
        }


        public List<Reviews> GetAll()
        {
            return context.Reviews.ToList();

        }
        public Reviews GetByID(int id)
        {
            return context.Reviews.FirstOrDefault(s => s.Id == id);

        }
        public List<Reviews> GetByUserId(string Id)
        {
            return context.Reviews.Where(w =>w.ApplicationUser.Id==Id).ToList();
        }
        public List<Reviews> GetByProductrId(int Id)
        {
            return context.Reviews.Where(w => w.ProductId == Id).ToList();
        }

        public void Update(int id, Reviews reviews)
        {
            Reviews old = GetByID(id);
            old.Body = reviews.Body;
            old.ApplicationUser.Id = reviews.ApplicationUser.Id;
            old.Stars = reviews.Stars;
            old.ProductId = reviews.ProductId;

            context.SaveChanges();

        }
        public void Add(Reviews reviews)
        {
            context.Reviews.Add(reviews);
            context.SaveChanges();

        }
        public void Delete(int id)
        {
            context.Reviews.Remove(GetByID(id));
            context.SaveChanges();
        }
    }
}