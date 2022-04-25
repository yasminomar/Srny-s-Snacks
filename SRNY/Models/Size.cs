using System.ComponentModel.DataAnnotations.Schema;

namespace SRNY.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string size { get; set; }
        public int Available { get; set; }
        public int Price { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
