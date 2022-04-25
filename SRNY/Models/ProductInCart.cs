using System.ComponentModel.DataAnnotations.Schema;

namespace SRNY.Models
{
    public class ProductInCart
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }

    }
}
