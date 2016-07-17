using ECommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Class_6_21
{
    public class ShoppingCartCountFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ECommerceRepository repo = new ECommerceRepository(Properties.Settings.Default.ConnStr);
            var session = context.HttpContext.Session.Contents["cartId"];
            if (session != null)
            {
                int cartid = (int)session;
                context.Controller.ViewBag.ShoppingCartCount = repo.GetCartCountById(cartid);
            }
            
        }
    }
}