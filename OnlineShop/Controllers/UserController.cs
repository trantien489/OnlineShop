using Microsoft.AspNet.Identity;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class UserController : UserBaseController
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Info(string message)
        {
            var userId = User.Identity.GetUserId();
            var user = UserManeger.FindById(userId);
            UpdateInfoUserModel result = new UpdateInfoUserModel();
            result.UserName = user.UserName;
            result.FirstName = user.FirstName;
            result.LastName = user.LastName;
            result.Email = user.Email;
            result.PhoneNumber = user.PhoneNumber;
            result.Address = user.Address;

            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;
            }
            return View(result);
        }

        [HttpPost]
        public ActionResult Info(UpdateInfoUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserManeger.FindByName(model.UserName);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
                DbContext.SaveChanges();
            }
            else
            {
                return View(model);
            }
            return RedirectToAction("Info", new { message = "Lưu thành công" });
        }

        public ActionResult Orders()
        {
            return View();
        }


        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}