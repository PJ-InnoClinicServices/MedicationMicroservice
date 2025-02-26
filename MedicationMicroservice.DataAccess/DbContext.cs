using MedicationMicroservice.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicationMicroservice.Application 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Substance> Substances { get; set; }
        
        public DbSet<Disease> Diseases { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Drug>()
                .HasMany(e => e.Substances)
                .WithMany(e => e.Drugs);
            
            modelBuilder.Entity<Substance>()
                .HasMany(e => e.Diseases)
                .WithMany(e => e.Substances);
        }
    }
}