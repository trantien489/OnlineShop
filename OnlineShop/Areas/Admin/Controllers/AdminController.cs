using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController: Controller
    {
        #region OwinContext
        private OwinContext mOwinContext;
        protected OwinContext OwinContext
        {
            get
            {
                if (mOwinContext == null)
                {
                    mOwinContext = new Microsoft.Owin.OwinContext();
                }

                return mOwinContext;
            }
        }
        #endregion
    }
}   