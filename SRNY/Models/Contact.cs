using System.ComponentModel.DataAnnotations;

namespace SRNY.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "first Name is Required")]
        public string Fname { get; set; }
        [Required(ErrorMessage = "last Name is Required")]
        public string Lname { get; set; }
        [Required(ErrorMessage = "email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "message is Required")]
        public string Message { get; set; }
    }
}