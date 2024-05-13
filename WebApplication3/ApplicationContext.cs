using Microsoft.EntityFrameworkCore;

namespace WebApplication3
{
    public class ApplicationContext:DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options) 
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id=1,Name="Ted",Age=35},
                new User { Id=2, Name="Bred",Age=40},
                new User { Id=3, Name="Tom",Age=30}
                );
        }
    }
}
