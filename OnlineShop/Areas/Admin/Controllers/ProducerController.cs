﻿using Microsoft.AspNet.Identity.Owin;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
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