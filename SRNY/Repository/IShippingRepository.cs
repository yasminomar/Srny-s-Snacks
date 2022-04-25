using SRNY.Models;
using System.Collections.Generic;

namespace SRNY.Repository
{
    public interface IShippingRepository
    {
        void Delete(int id);
        List<Shipping> GetAll();
        Shipping GetByID(int id);
        void Insert(Shipping Shipping);
        void Update(int id, Shipping Shipping);
    }
}