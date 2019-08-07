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

        [HttpPost("checkout")]
        public ActionResult<Dictionary<string, object>> Checkout(Dictionary<string, object> m)
        {
            return m;
        }
    }
}