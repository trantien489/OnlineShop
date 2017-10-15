using Microsoft.AspNet.Identity.Owin;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProducerController : AdminController
    {
        // GET: Admin/Producer
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(string name, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string _FileName = Path.GetFileName(image.FileName);
                    var index = _FileName.LastIndexOf('.');
                    var filename = Guid.NewGuid().ToString() + _FileName.Substring(index, _FileName.Count() - index);
                    var ImagePath = Server.MapPath("~/Photos/Producer");
                    if (!System.IO.File.Exists(ImagePath))
                    {
                        Directory.CreateDirectory(ImagePath);
                    }
                    string _path = Path.Combine(ImagePath, filename);
                    image.SaveAs(_path);
                    var producer = new Producer();
                    producer.Name = name;
                    producer.Image = _path;
                    var data = DbContext.Producers.Add(producer);
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
                        result = "Fail", error = e.ToString()
                    });
                }
            }
            return null;

        }


        [HttpGet]
        public JsonResult GetProducers()
        {
            try
            {
                return Json(DbContext.Producers.ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}