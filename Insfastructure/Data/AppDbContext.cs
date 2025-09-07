using library_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace library_api.Insfastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Example: Book-Author many-to-many
                modelBuilder.Entity<Book>()
                    .HasMany(b => b.Authors)
                    .WithMany(a => a.Books); 

                // Example: Book-Genre many-to-many
                modelBuilder.Entity<Book>()
                    .HasMany(b => b.Genres)
                    .WithMany(g => g.Books);
            }
        }
    }
