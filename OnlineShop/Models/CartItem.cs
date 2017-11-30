using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class CartItem
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public string GetPrice()
        {
            return Product.Price.Value.ToString("N0");
        }

        public string GetMoney()
        {
            return (Product.Price.Value * Quantity).ToString("N0");
        }

        public decimal GetPriceDecimal()
        {
            return  Product.Price.Value;
        }

        public decimal GetMoneyDecimal()
        {
            return (Product.Price.Value * Quantity);
        }
    }
}