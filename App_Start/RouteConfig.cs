﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebAppMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        //    routes.MapHttpRoute(
        //    name: "CorsPreflight",
        //    routeTemplate: "api/{controller}/{action}/{id}",
        //    defaults: new { id = RouteParameter.Optional },
        //    constraints: new { httpMethod = new HttpMethodConstraint("OPTIONS") }
        //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                
            );


        }
    }
}
