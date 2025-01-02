using Microsoft.EntityFrameworkCore;
using AppApi.Data.Models;

namespace AppApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<GroceryList> GroceryLists { get; set; }
    }
}
