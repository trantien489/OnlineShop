using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class Category: BaseModel
    {
       
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string MetaKeyword { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }

    }
}
