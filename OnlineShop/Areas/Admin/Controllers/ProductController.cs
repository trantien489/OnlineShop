using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : AdminController
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetProducts()
        {
            try
            {

                var products = DbContext.Products.Where(p => p.Status == true);
                var resultProduct = products.Select(p => new {
                    Id = p.Id,
                    Name = p.ProductName,
                    Producer = DbContext.Producers.Find(p.ProducerId) != null ? DbContext.Producers.Find(p.ProducerId).Name : "",
                    Quantity = p.Quantity,
                    Price = p.Price
                });
                return Json(resultProduct.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    result = "Fail",
                    error = e.ToString()
                });
            }
        }
    }
}