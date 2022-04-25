using System.Collections.Generic;

namespace SRNY.Models
{
    public class ProductViewModel
    {
        public int currentPageIndex { get; set; }
        public int pageCount { get; set; }
        public List<Product> ProductList { get; set; }


    }
}
