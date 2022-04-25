using SRNY.Models;
using System.Collections.Generic;

namespace SRNY.Repository
{
    public interface ISizeRepository
    {
        void Delete(int id);
        List<Size> GetAll();
        Size GetByID(int id);
        List<Size> GetByProductId(int id);
        List<Size> GetByProductID(int id);
        void Insert(Size Size);
        void Update(int id, Size Size);
    }
}