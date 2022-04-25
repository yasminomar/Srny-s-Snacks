using System.ComponentModel.DataAnnotations.Schema;

namespace SRNY.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public string Body { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

      public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Stars { get; set; }

    }
}
