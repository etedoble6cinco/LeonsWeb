using LeonsWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LeonsWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Quote> Queues { get; set; }
        public virtual DbSet<Promo> Promos { get; set; }
        public virtual DbSet<Service> Services { get; set; }        
    }
}