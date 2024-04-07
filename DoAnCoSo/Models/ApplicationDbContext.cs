using Microsoft.EntityFrameworkCore;

namespace DoAnCoSo.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<ClassAnimal> ClassAnimals { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<AnimalImage> AnimalImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .HasMany(a => a.AnimalImages)
                .WithOne(ai => ai.Animal)
                .HasForeignKey(ai => ai.AnimalId);
        }
    }
}