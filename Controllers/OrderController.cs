using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using sportsstore_api.Models;
using sportsstore_api.Repositories;

namespace sportsstore_api_dotnet_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository repository;

        public OrderController(IOrderRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public ActionResult<List<Order>> List()
        {
            var q = repository.Orders.Where(o => !o.Shipped);
            return q.ToList();
        }

        [HttpPost("checkout")]
        public ActionResult<Order> Checkout(Dictionary<string, object> m)
        {
            Order o = new Order {
                OrderID = 0,
                Name = m["name"].ToString(),
                Line1 = m["line1"].ToString(),
                Line2 = m["line2"].ToString(),
                Line3 = m["line3"].ToString(),
                City = m["city"].ToString(),
                State = m["state"].ToString(),
                Zip = m["zip"].ToString(),
                Country = m["country"].ToString(),
                GiftWrap = Convert.ToBoolean(m["giftwrap"])
            };
            Newtonsoft.Json.Linq.JArray cartLines = (Newtonsoft.Json.Linq.JArray)m["cartLines"];
            List<CartLine> lx = new List<CartLine>();

            foreach (var x in cartLines)
            {
                var product = x["product"];
                int quantity = (int)x["quantity"];
                CartLine c = new CartLine();
                c.Product = new Product {
                    ProductID = (int)product["productID"],
                    Name = product["name"].ToString(),
                    Description = product["description"].ToString(),
                    Price = (decimal)product["price"],
                    Category = product["category"].ToString()
                };
                c.Quantity = quantity;
                lx.Add(c);
            }

            o.Lines = lx;
            repository.SaveOrder(o);
            
            return o;
        }
    }
}