//using SRNY.Models;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;

//namespace SRNY.Models
//{
//    public class UniqueNameAttribute:ValidationAttribute
//    {
//        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//        {
//            string email= value.ToString();
//            SRNYContext db  = new SRNYContext();
//            ApplicationUser user = db.Users.FirstOrDefault(w => w.Email == email);
//            if(user == null)
//            {
//                return ValidationResult.Success;
//            }
//            return new ValidationResult("Email already Exist");
//        }
//    }
//}
