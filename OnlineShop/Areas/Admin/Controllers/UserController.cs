using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineShop.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetUsers()
        {
            try
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(DbContext));
                var roleId = roleManager.FindByName("User").Id;
                var users = UserManeger.Users.Include(u => u.Roles).Where(u => u.Roles.Any(r => r.RoleId == roleId) && u.Status == true).ToList();
                var result = users.Select(i => new
                {
                    CustomerId = i.CustomerId,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Phone = i.PhoneNumber,
                    Email = i.Email,
                    Address = i.Address,
                    JoinDate = i.JoinDate.ToString("d/M/yyyy"),
                    LockStatus = i.LockoutEnabled,
                    StatusString = StringHelper.GetStringLockStatus(i.LockoutEnabled),
                    UserName = i.UserName
                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    result = "Fail",
                    error = e.ToString()
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Update(int customerId, bool status)
        {
            try
            {
                var user = UserManeger.Users.FirstOrDefault( u=> u.CustomerId == customerId);
                user.LockoutEnabled = status;
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
                }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Delete(int customerId)
        {
            try
            {
                var user = UserManeger.Users.FirstOrDefault(u => u.CustomerId == customerId);
                user.Status = false;
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
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}