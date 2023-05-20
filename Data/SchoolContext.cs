using Autenticacion.Models;
using Microsoft.EntityFrameworkCore;

namespace Autenticacion.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
        {
        }

        public SchoolContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=SupermarketEF;Trusted_Connection=True");
        }
    }
}
