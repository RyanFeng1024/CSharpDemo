using RESTAPI.Models;

namespace RESTAPI.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    }
}
