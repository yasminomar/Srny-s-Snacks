using SRNY.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRNY.Repository
{
    public class ShippingRepository : IShippingRepository
    {
        SRNYContext context;
        public ShippingRepository(SRNYContext _context)
        {
            this.context = _context;
        }
        public List<Shipping> GetAll()
        {
            return context.Shipping.ToList();

        }
        public Shipping GetByID(int id)
        {
            return context.Shipping.FirstOrDefault(s => s.Id == id);

        }
        public void Insert(Shipping Shipping)
        {
            context.Shipping.Add(Shipping);
            context.SaveChanges();

        }
        public void Update(int id, Shipping Shipping)
        {
            Shipping old = GetByID(id);
            old.Towen = Shipping.Towen;
            old.Price = Shipping.Price;
            context.SaveChanges();

        }
        public void Delete(int id)
        {
            context.Shipping.Remove(GetByID(id));
            context.SaveChanges();
        }
    }
}
