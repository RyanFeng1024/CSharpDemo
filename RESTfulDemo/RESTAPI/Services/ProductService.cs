using RESTAPI.Models;
using RESTAPI.Repositories;

namespace RESTAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepo.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productRepo.GetByIdAsync(id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            // 可在此进行业务逻辑校验，示例简单略过
            return await _productRepo.AddAsync(product);
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            // 可在此进行业务逻辑校验，如检查Price是否合理
            return await _productRepo.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> SearchByNameAsync(string name)
        {
            return await _productRepo.GetProductsByNameAsync(name);
        }
    }
}
