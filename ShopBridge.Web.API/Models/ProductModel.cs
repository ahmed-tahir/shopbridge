using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBridge.Web.API.Models
{
    public class ProductModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}