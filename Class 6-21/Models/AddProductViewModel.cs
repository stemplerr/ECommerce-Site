using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Data;

namespace Class_6_21.Models
{
    public class AddProductViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}