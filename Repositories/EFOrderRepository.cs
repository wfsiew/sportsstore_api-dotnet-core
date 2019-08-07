using Microsoft.EntityFrameworkCore;
using System.Linq;
using sportsstore_api.Models;

namespace sportsstore_api.Repositories
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;

        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders
        {
            get
            {
                return context.Orders
                    .Include(o => o.Lines)
                    .ThenInclude(l => l.Product);
            }
        }

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }

            context.SaveChanges();
        }
    }
}