using System.ComponentModel.DataAnnotations.Schema;

namespace SRNY.Models
{
    public class CategoriesInProduct
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
