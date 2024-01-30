using Microsoft.EntityFrameworkCore;

namespace Redis.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().HasData(

                new Product()
                {
                    Id = 1,
                    Name = "Kalem",
                },
                new Product()
                {
                 Id = 2,
                 Name = "Kalem"
                },
                new Product()
                {
                 Id = 3,
                 Name = "Kalem"
                });

            base.OnModelCreating(modelBuilder);
        }

    }
}
