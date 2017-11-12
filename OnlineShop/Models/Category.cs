using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class Category: BaseModel
    {
       
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string MetaKeyword { get; set; }

        public string Image { get; set; }

        public virtual ICollection<CategoryProducer> CategoryProducers { get; set; }


    }
}
