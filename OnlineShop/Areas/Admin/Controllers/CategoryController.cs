using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using OnlineShop.Helper;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCategories()
        {
            try
            {
                var data = DbContext.Categories.Where(c => c.Status == true).Include(c => c.CategoryProducers).ToList();
                if (data.Count() == 0)
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                var response = data.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Producers = x.CategoryProducers.Where(c => c.Status == true).Select(a => new {
                        Name = DbContext.Producers.Find(a.ProducerId).Name,
                        Id = a.ProducerId
                    })
                }).ToList();
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public JsonResult Add(string name, int[] producersId, HttpPostedFileBase image)
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    var cate = new Category();
                    cate.Name = name;
                    cate.MetaKeyword = StringHelper.GetMetaTitle(name);
                    var data = DbContext.Categories.Add(cate);
                    foreach (var id in producersId)
                    {
                        var item = new CategoryProducer();
                        item.CategoryId = data.Id;
                        item.ProducerId = id;
                        DbContext.CategoryProducers.Add(item);
                    }
                    if (image != null)
                    {
                        var ImagePath = Server.MapPath("~/Photos/Category");
                        var filename = ImageHelper.SaveImage(image, ImagePath);
                        if (!System.IO.Directory.Exists(ImagePath))
                        {
                            Directory.CreateDirectory(ImagePath);
                        }
                        cate.Image = filename;
                    }
                    DbContext.SaveChanges();
                    return Json(new
                    {
                        result = "Success",
                        data = ""
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

        [HttpPost]
        public JsonResult Update(int id, string name, int[] producersId, HttpPostedFileBase image)
        {
            if (!string.IsNullOrEmpty(name) || producersId.Count() == 0)
            {
                try
                {
                    var cate = DbContext.Categories.Find(id);
                    if (cate != null)
                    {
                        cate.Name = name;
                        cate.MetaKeyword = StringHelper.GetMetaTitle(name);
                        var categoryProducers = DbContext.CategoryProducers.Where(c => c.CategoryId == id).ToList();
                        DbContext.CategoryProducers.RemoveRange(categoryProducers);
                        foreach (var item in producersId)
                        {
                            DbContext.CategoryProducers.Add(new CategoryProducer()
                            {
                                CategoryId = cate.Id,
                                ProducerId = item
                            });
                        }
                        if (image != null)
                        {
                            var ImagePath = Server.MapPath("~/Photos/Category");
                            var filename = ImageHelper.SaveImage(image, ImagePath);
                            if (!System.IO.Directory.Exists(ImagePath))
                            {
                                Directory.CreateDirectory(ImagePath);
                            }
                            if (!string.IsNullOrEmpty(cate.Image))
                            {
                                System.IO.File.Delete(Path.Combine(ImagePath, cate.Image));

                            }
                            cate.Image = filename;
                        }
                        DbContext.SaveChanges();

                        return Json(new
                        {
                            result = "Success",
                            data = ""
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
                    var cate = DbContext.Categories.Find(id);
                    if (cate != null)
                    {
                        var categoryProducers = DbContext.CategoryProducers.Where(c => c.CategoryId == id).ToList();
                        DbContext.CategoryProducers.RemoveRange(categoryProducers);

                        DbContext.Categories.Remove(cate);

                        DbContext.SaveChanges();
                        return Json(new
                        {
                            result = "Success",
                            data = ""
                        }, JsonRequestBehavior.AllowGet);
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
            }
            return null;

        }

        [HttpPost]
        public ActionResult CheckCategoryExist(string name)
        {
            try
            {
                var metaName = StringHelper.GetMetaTitle(name);
                var product = DbContext.Categories.FirstOrDefault(p => p.MetaKeyword == metaName);
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


        [HttpGet]
        public JsonResult GetProducesCategorie(int categoryId)
        {
            try
            {
                var data = DbContext.CategoryProducers.Where(c => c.Status == true && c.CategoryId == categoryId).Include(c => c.Producer).ToList();
                if (data.Count() == 0)
                {
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                var response = data.Select(x => new
                {
                    ProducerId = x.ProducerId,
                    ProducerName = x.Producer.Name,
                }).ToList();
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}