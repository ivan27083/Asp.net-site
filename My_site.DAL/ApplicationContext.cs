using Microsoft.EntityFrameworkCore;
using My_stie.Domainn.DomainModels;

namespace My_site.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Computer> Computers { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=My_site;Username=postgres;Password=971254");
        }
    }
}
