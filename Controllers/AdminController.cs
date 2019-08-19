using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using sportsstore_api.Models;
using sportsstore_api.Repositories;

namespace sportsstore_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        [HttpGet("products")]
        public ActionResult<List<Product>> List()
        {
            return repository.Products.ToList();
        }

        [HttpGet("product/{id}")]
        public ActionResult<Product> Edit(int id)
        {
            return repository.Products.FirstOrDefault(p => p.ProductID == id);
        }

        [HttpPut("product/{id}")]
        public ActionResult<Product> Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                return product;
            }
            
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public ActionResult<Dictionary<string, object>> Delete(int id)
        {
            Product o = repository.DeleteProduct(id);
            if (o != null)
            {
                return new Dictionary<string, object> { { "success", 1 } };
            }

            return BadRequest();
        }

        [HttpPost("product-seed")]
        public ActionResult<Dictionary<string, object>> SeedProduct() {
            for (int i = 0; i < 500; i++) {
                Product o = new Product {
                    Name = string.Format("Product - {0}", i),
                    Description = string.Format("Product Desc - {0}", i),
                    Price = 100.0M,
                    Category = "Soccer"
                };
                repository.SaveProduct(o);
            }

            return new Dictionary<string, object> { { "success", 1 } };
        }
    }
}