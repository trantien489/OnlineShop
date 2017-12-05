using Microsoft.AspNet.Identity;
using OnlineShop.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ActionResult Orders(int page = 1, int pageSize = 9)
        {
            var userId = User.Identity.GetUserId();
            var invoices = DbContext.Invoices.Where(i => i.Status == true).Include(i => i.ApplicationUser).Where(i => i.ApplicationUser.Id == userId)
                .OrderByDescending(p => p.CreatedDate).ToPagedList(page, pageSize);
            return View(invoices);
        }

        [HttpGet]
        public ActionResult GetInvoiceDetailbyInvoiceId(int invoiceId)
        {

            var invoiceDetails = DbContext.InvoiceDetails.Where(i => i.InvoiceId == invoiceId).Include(i => i.Product).ToList();

            ViewBag.InvoiceId = invoiceId;
            return View(invoiceDetails);
        }

        public ActionResult ChangePassword(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var result = UserManeger.ChangePassword(userId, model.Oldpass, model.NewPass);
                if (result.Succeeded)
                {
                    return RedirectToAction("ChangePassword", new { message = "Đổi mật khẩu thành công" });
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Mật khẩu không chính xác");
                    return View(model);

                    //return RedirectToAction("ChangePassword", new { message = "Mật khẩu không chính xác" });
                }

            }
            else
            {
                return View( model);
            }
        }
    }
}