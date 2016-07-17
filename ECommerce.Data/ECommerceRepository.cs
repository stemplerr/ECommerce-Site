using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data
{
    public class ECommerceRepository
    {
        private string _connString;
        public ECommerceRepository(string connString)
        {
            _connString = connString;
        }
        public IEnumerable<Product> GetProducts()
        {
            ECommerceDataContext context = ProductContext();
            return context.Products.ToList();
        }
        public IEnumerable<Product> GetProducts(int? categoryid)
        {
            ECommerceDataContext context = ProductContext();
            return context.Products.Where(p => p.Category.Id == categoryid).ToList();
        }

        public ECommerceDataContext ProductContext()
        {
            ECommerceDataContext context = new ECommerceDataContext();
            DataLoadOptions loadoptions = new DataLoadOptions();
            loadoptions.LoadWith<Product>(p => p.Category);
            loadoptions.LoadWith<Product>(p => p.Images);
            context.LoadOptions = loadoptions;
            return context;
        }
        
        public IEnumerable<Category> GetCategories()
        {
            ECommerceDataContext context = new ECommerceDataContext();
            DataLoadOptions loadoptions = new DataLoadOptions();
            loadoptions.LoadWith<Category>(c => c.Products);
            context.LoadOptions = loadoptions;
            return context.Categories.ToList();
        }
        //----------------- Admin Options ------------------------------------
        public void AddCategory(Category c)
        {
            ECommerceDataContext context = new ECommerceDataContext();
            context.Categories.InsertOnSubmit(c);
            context.SubmitChanges();
        }
        public void AddProduct(Product p)
        {
            ECommerceDataContext context = new ECommerceDataContext();
            context.Products.InsertOnSubmit(p);
            context.SubmitChanges();
        }
        public void AddImage(Image i)
        {
            ECommerceDataContext context = new ECommerceDataContext();
            context.Images.InsertOnSubmit(i);
            context.SubmitChanges();
        }
        //------------------- Shopping Cart Options ------------------------------
        public Product GetProductById(int productid)
        {
            ECommerceDataContext context = new ECommerceDataContext();
            return context.Products.First(p => p.Id == productid); 
        }
        public int GetNewCart ()
        {
            ECommerceDataContext context = new ECommerceDataContext();
            ShoppingCart cart = new ShoppingCart();
            context.ShoppingCarts.InsertOnSubmit(cart);
            context.SubmitChanges();
            return cart.Id;
        }
        public void AddToCart (int productid, int shoppingcartid)
        {
            ECommerceDataContext context = new ECommerceDataContext();
            ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = productid,
                    ShoppingCartId = shoppingcartid
                };
            context.ShoppingCartItems.InsertOnSubmit(item);
            context.SubmitChanges();
        }

        public ShoppingCart GetCartById (int cartid)
        {
            ECommerceDataContext context = new ECommerceDataContext();
            DataLoadOptions loadOptions = new DataLoadOptions();
            loadOptions.LoadWith<ShoppingCart>(c => c.ShoppingCartItems);
            context.LoadOptions = loadOptions;
            return context.ShoppingCarts.First(c => c.Id == cartid);
        }

        public int GetCartCountById (int cartid)
        {
            ShoppingCart cart = GetCartById(cartid);
            return cart.ShoppingCartItems.Count;
        }
        public IEnumerable<Product> GetCartProducts(int cartid)
        {
            ECommerceDataContext context = new ECommerceDataContext();
            ShoppingCart cart = GetCartById(cartid);
            List<Product> products = new List<Product>();
            foreach(ShoppingCartItem item in cart.ShoppingCartItems)
            {
                products.Add(context.Products.First(p => p.Id == item.ProductId));
            }
            return products;
        }
    }
}
