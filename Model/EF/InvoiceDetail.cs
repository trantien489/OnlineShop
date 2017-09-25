using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{
    public class InvoiceDetail:BaseModel
    {
        public InvoiceDetail()
        {
            this.Products = new HashSet<Product>();
        }

        public int? InvoiceId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Money { get; set; }


        public virtual Invoice Invoice { get; set; }

        public virtual Product Product { get; set; }



        public virtual ICollection<Product> Products { get; set; }
    }
}
