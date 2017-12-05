using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OnlineShop.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult HomePage(int page = 1, int pageSize = 9)
        {
            var products = DbContext.Products.Where(p =>p.Status == true).OrderByDescending(p => p.CreatedDate).ToPagedList(page, pageSize);
            ViewBag.ActionName = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            return View(products);
        }

        public ActionResult ContactCustom()
        {
            return View();
        }
    }
}