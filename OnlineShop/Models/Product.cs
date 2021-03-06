﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Models
{
    public class Product : BaseModel
    {
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        public int? ImageId { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public decimal? SalePrice { get; set; }

        [StringLength(100)]
        public string MetaKeyword { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        [Column(TypeName = "ntext")]
        public string Information { get; set; }

        public int? ViewCount { get; set; }

        public int? CategoryId { get; set; }

        public int? ProducerId { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
