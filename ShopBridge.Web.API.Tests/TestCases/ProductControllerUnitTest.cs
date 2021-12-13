using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopBridge.Web.API.Controllers;
using ShopBridge.Web.API.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Threading;
using ShopBridge.Web.API.BussinessAccess;
using System.Web.Http;
using ShopBridge.Web.API.Models;
using System.Web.Http.Results;

namespace ShopBridge.Web.API.Tests
{
    [TestClass]
    public class ProductControllerUnitTest
    {
        private readonly Mock<ProductManagement> _mockObject;
        private ProductController _controller;

        public ProductControllerUnitTest()
        {
            _mockObject = new Mock<ProductManagement>();
        }

        [TestMethod]
        public async Task Test_GetProductsAsync()
        {
            // Arrange
            List<Products> products = new List<Products>()
            {
                new Products(){ ID = 1, Name = "Product1", Description = "Description1", Price = 10.00, CreatedDate = DateTime.UtcNow, IsDeleted = false},
                new Products(){ ID = 2, Name = "Product2", Description = "Description2", Price = 20.00, CreatedDate = DateTime.UtcNow, IsDeleted = false},
                new Products(){ ID = 3, Name = "Product3", Description = "Description3", Price = 30.00, CreatedDate = DateTime.UtcNow, IsDeleted = false}
            };
            _mockObject.Setup(p => p.GetProductsAsync().Result).Returns(products);

            // Act
            _controller = new ProductController(_mockObject.Object);
            var response = await _controller.GetProductsAsync();

            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task Test_AddProductAsync()
        {
            // Arrange
            ProductModel model = new ProductModel();
            Products product = new Products() { Name = "Product1", Description = "Description1", Price = 25.00 };
            _mockObject.Setup(p => p.AddProductAsync(It.IsAny<ProductModel>()).Result).Returns(product);

            // Act
            _controller = new ProductController(_mockObject.Object);
            var response = await _controller.AddProductAsync(model);

            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task Test_UpdateProductAsync()
        {
            // Arrange
            ProductModel model = new ProductModel();
            int id = 0;
            Products product = new Products() { Name = "Product 1 updated", Description = "Description 1 updated", Price = 75.00 };
            _mockObject.Setup(p => p.UpdateProductAsync(It.IsAny<int>(), It.IsAny<ProductModel>()).Result).Returns(product);

            // Act
            _controller = new ProductController(_mockObject.Object);
            var response = await _controller.UpdateProductAsync(id, model);

            // Assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task Test_DeleteProductAsync()
        {
            // Arrange
            int id = 0;
            Products product = new Products() { Name = "Product name deleted", Description = "Deleted description 1", Price = 25.00 };
            _mockObject.Setup(p => p.DeleteProductAsync(It.IsAny<int>()).Result).Returns(product);

            // Act
            _controller = new ProductController(_mockObject.Object);
            var response = await _controller.DeleteProductAsync(id);
            var responseBody = (OkNegotiatedContentResult<Object>)response;

            // Assert
            Assert.IsNotNull(responseBody.Content);
            Assert.IsInstanceOfType(responseBody.Content, typeof(Object));
        }
    }
}
