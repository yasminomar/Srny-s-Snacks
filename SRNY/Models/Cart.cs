using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRNY.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public List<ProductInCart> Products { get; set; }
        public Order Order { get; set; }

    }
}
