using System.Linq;
using sportsstore_api.Models;

namespace sportsstore_api.Repositories
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}