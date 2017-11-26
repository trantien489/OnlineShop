using Microsoft.AspNet.Identity.Owin;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class BaseController : Controller
    {
        #region OwinContext
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
    }
}