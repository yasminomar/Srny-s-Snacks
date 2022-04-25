using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRNY.Models
{
    public class Order
    {
        public int Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("CartId")]
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        [ForeignKey("Shipping")]
        public int ShipId { get; set; }
        public Shipping Shipping { get; set; }
        //[Required(ErrorMessage = "Street is Required")]
        public string Street { get; set; }
        //[Required(ErrorMessage = "Apartment is Required")]
        public string Apartment { get; set; }
        public int zip { get; set; }
        //[Required(ErrorMessage = "First Name is Required")]
        //[MinLength(3, ErrorMessage = "FirstName must be more than 3 Charachter")]
        
        public string FirstName { set; get; }
        //[Required(ErrorMessage = "Lirst Name is Required")]
        //[MinLength(3, ErrorMessage = "LastName must be more than 3 Charachter")]
        public string LastName { set; get; }
        //[Required(ErrorMessage = "Phone is Required")]
        //[Phone]
        public string Phone { set; get; }
        //[Required(ErrorMessage = "Email is Required")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address.")]

        public string Email { set; get; }
        public string Status { set; get; }






    }
}
