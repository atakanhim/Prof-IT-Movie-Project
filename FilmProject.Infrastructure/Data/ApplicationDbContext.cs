using FilmProject.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FilmProject.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        DbSet<Movie> Movies { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<MovieCategoryMap> MovieCategoryMaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Comment>()
                .HasOne<Movie>(o => o.Movie)
                .WithMany(c => c.Comments)
                .HasForeignKey(b => b.MovieId);

            modelBuilder.Entity<Comment>()
                .HasOne<ApplicationUser>(o => o.AppUser)
                .WithMany()
                .HasForeignKey(b => b.userId);

            modelBuilder.Entity<MovieCategoryMap>()
                        .HasKey(bc => new { bc.MovieId, bc.CategoryId });

            modelBuilder.Entity<MovieCategoryMap>()
                .HasOne(bc => bc.Movie)
                .WithMany(b => b.MovieCategories)
                .HasForeignKey(bc => bc.MovieId);

            modelBuilder.Entity<MovieCategoryMap>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.MovieCategories)
                .HasForeignKey(bc => bc.CategoryId);


        }
    }
}