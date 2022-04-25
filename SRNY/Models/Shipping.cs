using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRNY.Models
{
    public class Shipping
    {
        public int Id { get; set; }
        public string Towen { get; set; }
        public int Price { get; set; }
        public List<Order> Orders { get; set; }


    }
}
