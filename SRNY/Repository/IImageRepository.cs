using SRNY.Models;
using System.Collections.Generic;

namespace SRNY.Repository
{
    public interface IImageRepository
    {
        void Delete(int id);
        List<Images> GetAll();
        Images GetByID(int id);
        List<Images> GetByProductId(int id);
        List<Images> GetByProductID(int id);
        Images GetMainImage(int id);
        void Insert(Images Images);
        void Update(int id, Images Images);
    }
}