using Microsoft.AspNet.Identity.Owin;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = "User")]
    public class UserBaseController : BaseController
    {
        // GET: UserBase
        public ActionResult Index()
        {
            return View();
        }

        protected ApplicationUserManager mUsermaneger;
        protected ApplicationUserManager UserManeger
        {
            get
            {
                if (mDbContext == null)
                {
                    mUsermaneger = this.HttpContext.GetOwinContext().Get<ApplicationUserManager>();
                }
                return mUsermaneger;
            }
        }
    }
}