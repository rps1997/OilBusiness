using Microsoft.EntityFrameworkCore;
using OilBusiness.Models;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace OilBusiness.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<BusinessType> businessTypes { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
