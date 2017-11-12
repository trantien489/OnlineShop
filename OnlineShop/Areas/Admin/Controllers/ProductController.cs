﻿using MiscUtil.Reflection;
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
                    Category = DbContext.Categories.Find(p.CategoryId) != null ? DbContext.Categories.Find(p.CategoryId).Name : "",
                    Quantity = p.Quantity,
                    Price = p.Price.Value.ToString("N0")+" VND",
                    PriceInt = p.Price.Value,
                    ProducerId = p.ProducerId,
                    CategoryId = p.CategoryId,
                    Image = p.Image,
                    Detail = p.Detail,
                    Information = p.Information
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
        [ValidateInput(false)]
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

        [HttpPost]
        public ActionResult CheckProductExist(string name)
        {
            try
            {
                var product = DbContext.Products.FirstOrDefault(p => p.ProductName == name);
                if (product == null)
                {
                    return Json(false);
                }
                else
                {
                    return Json(true);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Update(Product model, HttpPostedFileBase Image)
        {
            try
            {
                var product = DbContext.Products.Find(model.Id);
                if (product == null)
                {
                    return Json(new
                    {
                        result = "Fail",
                        error = $"Id {model.Id} not correct"
                    });
                }
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
                    if (!string.IsNullOrEmpty(product.Image))
                    {
                        System.IO.File.Delete(Path.Combine(ImagePath, product.Image));

                    }
                    product.Image = filename;
                }

                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.Detail = model.Detail;
                product.Information = model.Information;
                product.CategoryId = model.CategoryId;
                product.ProducerId = model.ProducerId;

                DbContext.SaveChanges();
                return Json(new
                {
                    result = "Success",
                    data = model
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

        [HttpGet]
        public JsonResult Delete(int id)
        {
            if (id != null)
            {
                try
                {
                    var product = DbContext.Products.Find(id);
                    product.Status = false;
                    if (!string.IsNullOrEmpty(product.Image))
                    {
                        var ImagePath = Server.MapPath("~/Photos/Product");
                        System.IO.File.Delete(Path.Combine(ImagePath, product.Image));
                        product.Image = null;
                    }
                    DbContext.SaveChanges();
                    return Json(new
                    {
                        result = "Success",
                        data = product
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
    }
   
}