using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            

            routes.MapRoute(
                 name: "GetProductByCategory",
                 url: "san-pham/{category}",
                 defaults: new { controller = "Product", action = "GetProductbyCategory", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
                 );

            routes.MapRoute(
              name: "ProductDetail",
              url: "san-pham/{product}/chi-tiet",
              defaults: new { controller = "Product", action = "ViewDetail", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
              );

            routes.MapRoute(
                 name: "GetProductByCategoryProducer",
                 url: "san-pham/{category}/{producer}",
                 defaults: new { controller = "Product", action = "GetProductByCategoryProducer", id = UrlParameter.Optional },
                 namespaces: new[] { "OnlineShop.Controllers" }
                 );

           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "HomePage", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }

            );

        }
    }
}
