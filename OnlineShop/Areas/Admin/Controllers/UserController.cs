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
                var users = UserManeger.Users.Include(u => u.Roles).Where(u => u.Roles.Any(r => r.RoleId == roleId)).ToList();
                var result = users.Select(i => new
                {
                    Id = i.Id,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Phone = i.PhoneNumber,
                    Email = i.Email,
                    Address = i.Address,
                    JoinDate = i.JoinDate,
                    Status = StringHelper.GetStringStatus(i.LockoutEnabled)
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

    }
}