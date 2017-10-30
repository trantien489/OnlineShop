using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
                var response = data.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
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
        public JsonResult Add(string name, int[] producersId)
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    var cate = new Category();
                    cate.Name = name;
                    var data = DbContext.Categories.Add(cate);
                    foreach (var id in producersId)
                    {
                        var item = new CategoryProducer();
                        item.CategoryId = data.Id;
                        item.ProducerId = id;
                        DbContext.CategoryProducers.Add(item);
                    }
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

        [HttpPost]
        public JsonResult Update(int id, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    var cate = DbContext.Categories.Find(id);
                    if (cate != null)
                    {
                        cate.Name = name;
                        DbContext.SaveChanges();
                        return Json(new
                        {
                            result = "Success",
                            data = cate
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
                        cate.Status = false;
                        DbContext.SaveChanges();
                        return Json(new
                        {
                            result = "Success",
                            data = cate
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
    }
}