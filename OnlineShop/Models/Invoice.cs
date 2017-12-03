using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class Invoice : BaseModel
    {

        public int InvoiceDetailId { get; set; }

        public decimal Total { get; set; }

        //public string UserId { get; set; }

        public int? InvoiceStatus { get; set; }

        public string NameReceive { get; set; }

        public string PhoneReceive { get; set; }

        public string AddressReceive { get; set; }

        public string EmailReceive { get; set; }



        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

}
