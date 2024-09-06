using ApiTest.Entity.Entites;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Entity
{
    //DbContext File to Configure the InMemory Database
    public class AppDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Products");
        }
        public DbSet<Product> Products { get; set; }
    }
}
