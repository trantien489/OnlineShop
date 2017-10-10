using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class CategoryProducer : BaseModel
    {
        public int ProducerId { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Producer Producer { get; set; } 

        public ICollection<Product> Products { get; set; }
    }
}