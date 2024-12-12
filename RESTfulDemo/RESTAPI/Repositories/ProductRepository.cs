using RESTAPI.Common;
using RESTAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RESTAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            // 返回虚拟数据
            return new Product(id, "P"+id, decimal.Parse("12.5"));
            
            // return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return new List<Product> {
                new Product(1, "P1", decimal.Parse("11.1")),
                new Product(2, "P2", decimal.Parse("12.1")),
                new Product(3, "P3", decimal.Parse("13.1")),
                new Product(4, "P4", decimal.Parse("14.1")),
                new Product(5, "P5", decimal.Parse("15.1")),
                new Product(6, "P6", decimal.Parse("16.1")),
                new Product(7, "P7", decimal.Parse("17.1")),
                new Product(8, "P8", decimal.Parse("18.1"))
            };

            // return await _context.Products.ToListAsync();
        }

        public async Task<Product> AddAsync(Product entity)
        {
            // _context.Products.Add(entity);
            // await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Product?> UpdateAsync(Product entity)
        {
            //var existing = await _context.Products.FindAsync(entity.Id);
            //if (existing == null)
            //    return null;

            //existing.Name = entity.Name;
            //existing.Price = entity.Price;
            //await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //var existing = await _context.Products.FindAsync(id);
            //if (existing == null)
            //    return false;

            //_context.Products.Remove(existing);
            //await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            return await _context.Products
                .Where(p => p.Name != null && p.Name.Contains(name))
                .ToListAsync();
        }
    }
}
