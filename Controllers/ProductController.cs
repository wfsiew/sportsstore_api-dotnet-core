using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using sportsstore_api.Models;
using sportsstore_api.Repositories;
using sportsstore_api.Models.ViewModels;

namespace sportsstore_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository repository;
        private int PageSize = 4;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        [HttpGet("{page}")]
        public ActionResult<ProductsListViewModel> List(int page = 1)
        {
            var o = new ProductsListViewModel
            {
                Products = repository.Products
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            };

            return o;
        }

        [HttpGet("{category}/{page}")]
        public ActionResult<ProductsListViewModel> List(string category, int page = 1)
        {
            var o = new ProductsListViewModel
            {
                Products = repository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                      repository.Products.Count() :
                      repository.Products.Where(e =>
                        e.Category == category).Count()
                },
                CurrentCategory = category
            };

            return o;
        }

        [HttpGet("categories")]
        public ActionResult<List<string>> CategoryList()
        {
            return repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x).ToList();
        }
    }
}