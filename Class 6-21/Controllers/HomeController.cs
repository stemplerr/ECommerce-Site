using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Data;
using Class_6_21.Models;

namespace Class_6_21.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult test ()
        {
            return View();
        }
        public ActionResult Index(int? categoryid)
        {
            ECommerceRepository repo = new ECommerceRepository(Properties.Settings.Default.ConnStr);
            HomeViewModel vm = new HomeViewModel();
            if (categoryid == null)
            {
                vm.Products = repo.GetProducts();
            }
            else
            {
                vm.Products = repo.GetProducts(categoryid);

            }
            vm.Categories = repo.GetCategories();
            return View(vm);
        }
        public ActionResult ProductPage(int productid)
        {
            ProductPageViewModel vm = new ProductPageViewModel();
            ECommerceRepository repo = new ECommerceRepository(Properties.Settings.Default.ConnStr);
            vm.Product = repo.GetProductById(productid);
            return View(vm);
        }
        public void AddToCart(int productid)
        {
            int cartid;
            ECommerceRepository repo = new ECommerceRepository(Properties.Settings.Default.ConnStr);
            if (Session["cartId"] != null)
            {
                cartid = (int)Session["cartId"];    
            }
            else
            {
                cartid = repo.GetNewCart();
                Session["cartId"] = cartid;
            }
            repo.AddToCart(productid, cartid);
            //return Json("foobar", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShoppingCart()
        {
            ECommerceRepository repo = new ECommerceRepository(Properties.Settings.Default.ConnStr);
            int cartid = (int)Session["cartId"];
            ShoppingCartViewModel vm = new ShoppingCartViewModel();
            vm.Products = repo.GetCartProducts(cartid);
            return View(vm);
        }
    }
}
