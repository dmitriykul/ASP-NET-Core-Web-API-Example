using Microsoft.EntityFrameworkCore;
using WorkersFeature.Models;

namespace WorkersFeature
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>()
                .Property<long>("PersonId");

            modelBuilder.Entity<Skill>()
                .HasOne(p => p.Person)
                .WithMany(s => s.Skills)
                .HasForeignKey(f => f.PersonId);

            modelBuilder.Entity<Person>()
                .HasMany(s => s.Skills)
                .WithOne(p => p.Person);
        }
    }
}