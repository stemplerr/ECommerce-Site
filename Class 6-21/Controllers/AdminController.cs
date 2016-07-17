using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Data;
using System.IO;
using Class_6_21.Models;
using System.Web.Security;

namespace Class_6_21.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertCategory(Category c)
        {
            ECommerceRepository repo = new ECommerceRepository(Properties.Settings.Default.ConnStr);
            repo.AddCategory(c);
            return Redirect("/");
        }
        public ActionResult AddProduct()
        {
            ECommerceRepository repo = new ECommerceRepository(Properties.Settings.Default.ConnStr);
            AddProductViewModel vm = new AddProductViewModel();
            vm.Categories = repo.GetCategories();
            return View(vm);
        }
        [HttpPost]
        public ActionResult InsertProduct(Product p, HttpPostedFileBase image)
        {
            ECommerceRepository repo = new ECommerceRepository(Properties.Settings.Default.ConnStr);
            string filename = Guid.NewGuid() + Path.GetExtension(image.FileName);
            image.SaveAs(Server.MapPath("~/Images/") + filename);
            repo.AddProduct(p);
            repo.AddImage(new Image
                {
                    ImageFileName = filename,
                    ProductId = p.Id,
                });
            return Redirect("/");
        }

        //------------------------------Account Functions---------------------------------------
        

        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult LogInAdminUser(string username, string password)
        {
           AdminRepository repo = new AdminRepository(Properties.Settings.Default.ConnStr);
           AdminUser au = repo.LogIn(username, password);
           if (au == null)
            {
                return Redirect("/admin/login");
            }
            FormsAuthentication.SetAuthCookie(username, true);
            return Redirect("/");
        }
        
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}
