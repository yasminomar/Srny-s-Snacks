using SRNY.Models;
using System.Collections.Generic;

namespace SRNY.Repository
{
    public interface IReviewRepository
    {
        void Add(Reviews reviews);
        void Delete(int id);
        List<Reviews> GetAll();
        Reviews GetByID(int id);
        void Update(int id, Reviews reviews);
        List<Reviews> GetByUserId(string Id);
        List<Reviews> GetByProductrId(int Id);
    }
}