using System.ComponentModel.DataAnnotations.Schema;

namespace SRNY.Models
{
    public class Images
    {
        public int Id { get; set; }
        public string image { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
