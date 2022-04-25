using SRNY.Models;
using System.ComponentModel.DataAnnotations;

namespace SRNY.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Email Address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Field must be Email Pattern")]
        //[UniqueName]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Not Matched")]
        public string ConfirmPassword { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}