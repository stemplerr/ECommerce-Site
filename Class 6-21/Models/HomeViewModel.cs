using ECommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Class_6_21.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
        //public string Message { get; set; }
    }
}