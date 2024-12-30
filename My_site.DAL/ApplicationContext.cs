using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using My_site.DAL.Configurations;
using My_site.DAL.Entities;
using My_site.DAL.Repositories;

namespace My_site.DAL
{
    public class ApplicationContext(
        DbContextOptions<ApplicationContext> options,
        IOptions<AuthorizationOptions> authOptions) : DbContext (options)
    {
        public DbSet<ComputerEntity> Computers { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<RoleEntity> Roles { get; set; } = null!;
        public DbSet<UserActionLog> UserActionLogs { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=My_site;Username=postgres;Password=971254", b =>
                b.MigrationsAssembly("My_site.DAL"));
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authOptions.Value));
            modelBuilder.ApplyConfiguration(new UserActionLogConfiguration());
        }
    }
}