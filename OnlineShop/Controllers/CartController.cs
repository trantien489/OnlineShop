using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CartController : BaseController
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetInfoCart()
        {
            List<CartItem> carts = Session["Cart"] as List<CartItem>;
            if (carts != null)
            {
                var response = carts.Select(c => new {
                    Product = c.Product,
                    Image = c.Product.Image,
                    Price = c.GetPrice(),
                    Money = c.GetMoney(),
                    Quantity = c.Quantity
                });
                return Json(
                    new
                    {
                        ItemCount = carts.Sum(p => p.Quantity),
                        Total = carts.Sum(c => c.Product.Price.Value * c.Quantity).ToString("N0"),
                        carts = response
                    }, JsonRequestBehavior.AllowGet
                );
            }
            else
            {
                return Json(
                    new
                    {
                        ItemCount = 0,
                    }, JsonRequestBehavior.AllowGet
                );

            }
        }

        [HttpPost]
        public void AddtoCart(int productId)
        {
            //Lấy lại danh sách sách chọn ở trong Session
            List<CartItem> carts = new List<CartItem>();
            if (Session["Cart"] != null)
            {
                carts = Session["Cart"] as List<CartItem>;
            }

            //tìm xem đã có sách trong giỏ hàng
            CartItem item = carts.SingleOrDefault(p => p.Product.Id == productId);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                carts.Add(new CartItem
                {
                    Product = DbContext.Products.Find(productId),
                    Quantity = 1
                });
            }

            //ghi nhận Session
            Session["Cart"] = carts;
            //trả về tổng số lượng hàng hòa            
            //return Json(
            //    new
            //    {
            //        ItemCount = carts.Sum(p => p.Quantity),
            //        Total = carts.Sum(p => p.Quantity * p.Product.Price)
            //    }
            //);
        }
        
        [HttpPost]
        public void SubtractionCart(int productId)
        {
            List<CartItem> carts = Session["Cart"] as List<CartItem>;
            CartItem item = carts.SingleOrDefault(p => p.Product.Id == productId);
            if (item != null && item.Quantity > 1)
            {
                item.Quantity--;
            }
            Session["Cart"] = carts;
        }

        [HttpPost]
        public void DeleteCart(int productId)
        {
            List<CartItem> carts = Session["Cart"] as List<CartItem>;
            CartItem item = carts.SingleOrDefault(p => p.Product.Id == productId);
            if (item != null)
            {
                carts.Remove(item);
            }
            Session["Cart"] = carts;
        }

    }
}