using SRNY.Models;
using System.Collections.Generic;
using System.Linq;

namespace SRNY.Repository
{
    public class ContactRepository : IContactRepository
    {
        SRNYContext context;
        public ContactRepository(SRNYContext _context)
        {
            this.context = _context;
        }
        public List<Contact> GetAll()
        {
            return context.Contact.ToList();

        }
        public Contact GetByID(int id)
        {
            return context.Contact.FirstOrDefault(s => s.Id == id);

        }

        public void Update(int id, Contact Contact)
        {
            Contact old = GetByID(id);
            old.Fname = Contact.Fname;
            old.Lname = Contact.Lname;
            old.Email = Contact.Email;
            old.Message = Contact.Message;

            context.SaveChanges();

        }
        public void Add(Contact Contact)
        {
            context.Contact.Add(Contact);
            context.SaveChanges();

        }
        public void Delete(int id)
        {
            context.Contact.Remove(GetByID(id));
            context.SaveChanges();

        }
    }
}
