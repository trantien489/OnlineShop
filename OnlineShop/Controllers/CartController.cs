using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CartController : UserBaseController
    {
        // GET: Cart
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckOut()
        {
            var user = UserManeger.FindById(User.Identity.GetUserId());
            return View(user);
        }

        [HttpPost]
        public ActionResult FinsishCheckOut(Invoice invoice)
        {
            var user = UserManeger.FindById(User.Identity.GetUserId());
            var carts = Session["Cart"] as List<CartItem>;
            var total = carts.Sum(c => c.Product.Price.Value * c.Quantity);

            invoice.Total = total >= 1000000 ? total : total + (total / 100) * 5;
            invoice.ApplicationUser = user;
            invoice.InvoiceStatus = 0;
            var invoiceId = DbContext.Invoices.Add(invoice).Id;


            var invoiceDetails = new List<InvoiceDetail>();
            foreach (var item in carts)
            {
                var invoiceDetail = new InvoiceDetail();
                invoiceDetail.InvoiceId = invoiceId;
                invoiceDetail.ProductId = item.Product.Id;
                invoiceDetail.Quantity = item.Quantity;
                invoiceDetail.Money = item.GetMoneyDecimal();
                invoiceDetails.Add(invoiceDetail);
            }

            DbContext.InvoiceDetails.AddRange(invoiceDetails);

            DbContext.SaveChanges();
            Session["Cart"] = null;
            return View("CheckOutSuccess");
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
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