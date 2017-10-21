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
                    var producer = new Producer();
                    if (image != null)
                    {
                        string _FileName = Path.GetFileName(image.FileName);
                        var index = _FileName.LastIndexOf('.');
                        var filename = Guid.NewGuid().ToString() +
                                       _FileName.Substring(index, _FileName.Count() - index);
                        var ImagePath = Server.MapPath("~/Photos/Producer");
                        if (!System.IO.Directory.Exists(ImagePath))
                        {
                            Directory.CreateDirectory(ImagePath);
                        }
                        string _path = Path.Combine(ImagePath, filename);
                        image.SaveAs(_path);
                        producer.Image = filename;
                    }

                    producer.Name = name;
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
                        result = "Fail",
                        error = e.ToString()
                    });
                }
            }
            return null;

        }

        [HttpGet]
        public JsonResult Delete(int id)
        {
            if (id != null)
            {
                try
                {
                    var item = DbContext.Producers.Find(id);
                    item.Status = false;
                    if (!string.IsNullOrEmpty(item.Image))
                    {
                        var ImagePath = Server.MapPath("~/Photos/Producer");
                        System.IO.File.Delete(Path.Combine(ImagePath, item.Image));
                        item.Image = null;
                    }
                    DbContext.SaveChanges();
                    return Json(new
                    {
                        result = "Success",
                        data = item
                    }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    return Json(new
                    {
                        result = "Fail",
                        data = e.ToString()
                    }, JsonRequestBehavior.AllowGet);
                }

            }
            return null;
        }

        [HttpPost]
        public JsonResult Update(int id, string name, HttpPostedFileBase image)
        {

            try
            {
                var producer = DbContext.Producers.Find(id);
                if (producer != null )
                {
                    if (image != null)
                    {
                        string _FileName = Path.GetFileName(image.FileName);
                        var index = _FileName.LastIndexOf('.');
                        var filename = Guid.NewGuid().ToString() +
                                       _FileName.Substring(index, _FileName.Count() - index);
                        var ImagePath = Server.MapPath("~/Photos/Producer");
                        if (!System.IO.Directory.Exists(ImagePath))
                        {
                            Directory.CreateDirectory(ImagePath);
                        }
                        string _path = Path.Combine(ImagePath, filename);
                        image.SaveAs(_path);
                        if (!string.IsNullOrEmpty(producer.Image))
                        {
                            System.IO.File.Delete(Path.Combine(ImagePath, producer.Image));

                        }
                        producer.Image = filename;
                    }
                    producer.Name = name;
                    DbContext.SaveChanges();
                    return Json(new
                    {
                        result = "Success",
                        data = producer
                    });
                }
                
            }
            catch (Exception e)
            {
                return Json(new
                {
                    result = "Fail",
                    error = e.ToString()
                });
            }
            return null;
        }


        [HttpGet]
        public JsonResult GetProducers()
        {
            try
            {
                return Json(DbContext.Producers.Where(p => p.Status == true).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
