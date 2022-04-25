using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRNY.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string SubName { get; set; }

        public List<CategoriesInProduct> Categories { get; set; }
        public List <Size> Sizes { get; set; }
        public List<Images> Images { get; set; }
        public List<Reviews> Reviews { get; set; }
        public List<ProductInCart> Products { get; set; }








    }
}
