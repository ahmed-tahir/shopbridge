using ShopBridge.Web.API.BussinessAccess;
using ShopBridge.Web.API.Data;
using ShopBridge.Web.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ShopBridge.Web.API.Controllers
{
    //[Authorize]
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly ProductManagement _businessObject;

        public ProductController()
        {
            _businessObject = new ProductManagement();
        }

        public ProductController(ProductManagement product)
        {
            _businessObject = product;
        }

        // GET api/product
        [HttpGet]
        [ResponseType(typeof(List<ProductModel>))]
        public async Task<IHttpActionResult> GetProductsAsync()
        {
            try
            {
                var products = await _businessObject.GetProductsAsync();
                return Ok(products);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // GET api/product/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetProductByIDAsync(int id)
        {
            try
            {
                var product = await _businessObject.GetProductByIDAsync(id);
                if(product != null)
                    return Ok(product);
                return Ok<Object>(new { message = $"No product was found with id {id}" });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/product
        [HttpPost]
        //[Route("add")]
        public async Task<IHttpActionResult> AddProductAsync(ProductModel model)
        {
            try
            {
                var product = await _businessObject.AddProductAsync(model);
                return Created<Products>("", product);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/product/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> UpdateProductAsync(int id, [FromBody]ProductModel model)
        {
            try
            {
                if (model == null) return BadRequest($"Request Body is empty");
                var product = await _businessObject.UpdateProductAsync(id, model);
                if(product != null)
                {
                    return Ok<Object>(new { message = $"Product with product id {id} was successfully updated" });
                }
                return BadRequest($"Product with product id {id} does not exist");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/product/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteProductAsync(int id)
        {
            try
            {
                var result = await _businessObject.DeleteProductAsync(id);
                if (result != null)
                {
                    return Ok<Object>(new { message = $"Product with id {id} was successfully deleted"});
                }
                return BadRequest($"Product with product id {id} does not exist");
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
