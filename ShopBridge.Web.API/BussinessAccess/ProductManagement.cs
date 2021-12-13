using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ShopBridge.Web.API.Controllers;
using ShopBridge.Web.API.Data;
using ShopBridge.Web.API.Models;

namespace ShopBridge.Web.API.BussinessAccess
{
    public class ProductManagement
    {

        private readonly ProductDbContext _dbContext;

        public ProductManagement()
        {
            _dbContext = new ProductDbContext();
        }

        public virtual async Task<List<Products>> GetProductsAsync()
        {
            List<Products> products = null;
            try
            {
                products = await _dbContext.Products.ToListAsync();
                return products;
            }
            catch(Exception ex)
            {
                return products;
            }
        }

        public virtual async Task<Products> GetProductByIDAsync(int id)
        {
            Products product = null;
            try
            {
                product = await _dbContext.Products.Where(f => f.ID == id).FirstOrDefaultAsync();
                return product;
            }
            catch(Exception ex)
            {
                return product;
            }
        }

        public virtual async Task<Products> AddProductAsync(ProductModel model)
        {
            Products product = null;
            Products result = null;
            try
            {
                product = new Products()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsAvailable = true,
                    IsDeleted = false
                };
                result = _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();
                return result;
            }
            catch(Exception ex)
            {
                return result;
            }
        }

        public virtual async Task<Products> UpdateProductAsync(int id, ProductModel model)
        {
            Products product = null;
            try
            {
                product = await _dbContext.Products.Where(p => p.ID == id).FirstOrDefaultAsync();
                if (product != null)
                {
                    product.Name = (String.IsNullOrEmpty(model.Name)) ? product.Name : model.Name;
                    product.Description = (String.IsNullOrEmpty(model.Description)) ? product.Description : model.Description;
                    product.Price = (model.Price == 0) ? product.Price : model.Price;
                    product.UpdatedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                    return product;
                }
                return product;
            }
            catch(Exception ex)
            {
                return product;
            }
        }

        public virtual async Task<Products> DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.Where(p => p.ID == id).FirstOrDefaultAsync();
            if(product != null)
            {
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                return product;
            }
            return product;
        }
    }
}