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

        [HttpPost]
        public JsonResult AddtoCart(int productId)
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
            return Json(
                new
                {
                    ItemCount = carts.Sum(p => p.Quantity),
                    Total = carts.Sum(p => p.Quantity * p.Product.Price)
                }
            );
        }
    }
}