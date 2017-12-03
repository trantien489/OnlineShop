using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        #region Database Context
        protected ApplicationDbContext mDbContext;
        protected ApplicationDbContext DbContext
        {
            get
            {
                if (mDbContext == null)
                {
                    mDbContext = this.HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                }
                mDbContext.Configuration.ProxyCreationEnabled = false;
                return mDbContext;
            }
        }
        #endregion

        #region User Manager
        protected ApplicationUserManager mUsermaneger;
        protected ApplicationUserManager UserManeger
        {
            get
            {
                if (mUsermaneger == null)
                {
                    mUsermaneger = this.HttpContext.GetOwinContext().Get<ApplicationUserManager>();
                }
                return mUsermaneger;
            }
        }
        #endregion
    }
}