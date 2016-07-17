using ECommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Class_6_21
{
    public class LayoutPageActionFilterAttribute:  ActionFilterAttribute
    {
        public override void  OnActionExecuting (ActionExecutingContext filtercontext)
        {
        ECommerceRepository repo = new ECommerceRepository(Properties.Settings.Default.ConnStr);
        filtercontext.Controller.ViewBag.IsLoggedIn = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }
}