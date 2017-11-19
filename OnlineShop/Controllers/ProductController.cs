using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : UserController
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetProductbyCategory(string category, int page = 1, int pageSize = 3)
        {
            try
            {
                var categoryId = DbContext.Categories.Where(c => c.MetaKeyword == category).First().Id;
                var products = DbContext.Products.Where(p => p.Status == true && p.CategoryId == categoryId).OrderBy(p => p.CreatedDate).ToPagedList(page, pageSize);
                ViewBag.ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
                ViewBag.ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                ViewBag.Category = category;
                return View("~/Views/Home/HomePage.cshtml", products);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult GetProductByCategoryProducer(string category, string producer, int page = 1, int pageSize = 3)
        {
            try
            {
                var categoryId = DbContext.Categories.Where(c => c.MetaKeyword == category).First().Id;
                var producerId = DbContext.Producers.Where(c => c.Name == producer).First().Id;
                var products = DbContext.Products.Where(p => p.Status == true && p.CategoryId == categoryId && p.ProducerId == producerId).
                    OrderBy(p => p.CreatedDate).ToPagedList(page, pageSize);
                ViewBag.ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
                ViewBag.ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                ViewBag.Category = category;
                ViewBag.Producer = producer;

                return View("~/Views/Home/HomePage.cshtml", products);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public ActionResult ViewDetail(string category, string producer, int id)
        {

        }
    }
}