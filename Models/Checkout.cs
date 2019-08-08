using System.Collections.Generic;

namespace sportsstore_api.Models
{
    public class Checkout
    {
        public Order Order { get; set; }
        public List<CartLine> Lines { get; set; }
    }
}