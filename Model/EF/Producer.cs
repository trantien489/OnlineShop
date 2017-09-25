using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class Producer:BaseModel
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
