﻿using Microsoft.AspNet.Identity.Owin;
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
        public JsonResult Add(string name, IEnumerable<HttpPostedFileBase> image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var file = image.FirstOrDefault();
                    var producer = new Producer();
                    if (file != null)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        var index = _FileName.LastIndexOf('.');
                        var filename = Guid.NewGuid().ToString() + _FileName.Substring(index, _FileName.Count() - index);
                        var ImagePath = Server.MapPath("~/Photos/Producer");
                        if (!System.IO.Directory.Exists(ImagePath))
                        {
                            Directory.CreateDirectory(ImagePath);
                        }
                        string _path = Path.Combine(ImagePath, filename);
                        file.SaveAs(_path);
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