using SRNY.Models;
using System.Collections.Generic;

namespace SRNY.Repository
{
    public interface IContactRepository
    {
        void Add(Contact Contact);
        void Delete(int id);
        List<Contact> GetAll();
        Contact GetByID(int id);
        void Update(int id, Contact Contact);
    }
}