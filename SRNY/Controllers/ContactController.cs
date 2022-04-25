using Microsoft.AspNetCore.Mvc;
using SRNY.Models;
using SRNY.Repository;
using System.Collections.Generic;
using System.Linq;

namespace SRNY.Controllers
{
    public class ContactController : Controller
    {
        IContactRepository ContactRepository;
        public ContactController(IContactRepository contactRepo)
        {
            this.ContactRepository = contactRepo;
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact con)
        {
            if (con.Fname != null && con.Lname != null && con.Email != null && con.Message != null)
            {
                ContactRepository.Add(con);
                return RedirectToAction("Index", "Home");
            }
            return View("Contact", con);

        }
    }
}