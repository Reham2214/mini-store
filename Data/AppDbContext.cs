using Microsoft.EntityFrameworkCore;
using mini_store.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace mini_store.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        //Tabels
        public DbSet<Product> Products {get; set;}
        public DbSet<Category> Categories {get; set;}
    }
}