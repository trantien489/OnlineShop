using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class AdminInfoController : AdminController
    {
        // GET: Admin/AdminInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ChangePassword( string oldPass, string newPass)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var result = UserManeger.ChangePassword(userId, oldPass, newPass);
                if (result.Succeeded)
                {
                    return Json(new
                    {
                        result = "Success",
                        data = ""
                    });
                }
                else
                {
                    return Json(new
                    {
                        result = "Fail",
                        error = "Mật khẩu không chính xác"
                    }, JsonRequestBehavior.AllowGet);
                }

            }
            catch(Exception e)
            {

                return Json(new
                {
                    result = "Fail",
                    error = e.ToString()
                }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}