using Microsoft.EntityFrameworkCore;
using RESTAPI.Models;
using System.Collections.Generic;

namespace RESTAPI.Common
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
    }
}
