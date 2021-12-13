using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopBridge.Web.API.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
        {

        }

        public DbSet<Products> Products { get; set; }
    }
}