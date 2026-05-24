using Microsoft.EntityFrameworkCore;
using AssignmentFinals.Models;

namespace AssignmentFinals.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Keep ONLY the Products table definition
        public DbSet<Product> Products { get; set; }
    }
}