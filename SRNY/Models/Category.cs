using System.Collections.Generic;

namespace SRNY.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoriesInProduct> Products { get; set; }

    }
}
