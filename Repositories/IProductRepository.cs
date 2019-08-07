using System.Linq;
using sportsstore_api.Models;

namespace sportsstore_api.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productID);
    }
}