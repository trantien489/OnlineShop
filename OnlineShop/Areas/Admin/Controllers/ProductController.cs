using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

                var products = DbContext.Products.ToList().Where(p => p.Status == true);
                if (products.Count() == 0)
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
                var resultProduct = products.Select(p => new {
                    Id = p.Id,
                    Name = p.ProductName,
                    Producer = DbContext.Producers.Find(p.ProducerId) != null ? DbContext.Producers.Find(p.ProducerId).Name : "",
                    Quantity = p.Quantity,
                    Price = p.Price.Value.ToString("N0"),
                    ProducerId = p.ProducerId,
                    CategoryId = p.CategoryId,
                    Image = p.Image
                }).ToList();
                return Json(resultProduct, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public JsonResult Add(Product model, HttpPostedFileBase Image)
        {
            try
            {
                if (Image != null)
                {
                    string _FileName = Path.GetFileName(Image.FileName);
                    var index = _FileName.LastIndexOf('.');
                    var filename = Guid.NewGuid().ToString() +
                                   _FileName.Substring(index, _FileName.Count() - index);
                    var ImagePath = Server.MapPath("~/Photos/Product");
                    if (!System.IO.Directory.Exists(ImagePath))
                    {
                        Directory.CreateDirectory(ImagePath);
                    }
                    string _path = Path.Combine(ImagePath, filename);
                    Image.SaveAs(_path);
                    model.Image = filename;
                }
                var data = DbContext.Products.Add(model);
                DbContext.SaveChanges();
                return Json(new
                {
                    result = "Success",
                    data = data
                });
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