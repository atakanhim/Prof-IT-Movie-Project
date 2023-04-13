using FilmProject.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FilmProject.Domain.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        DbSet<Movie> Movies { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .HasOne<Category>(o => o.MovieCategory)
                .WithMany(c => c.CategoryMovies)
                .HasForeignKey(b => b.CategoryId);


            modelBuilder.Entity<Comment>()
                .HasOne<Movie>(o => o.Movie)
                .WithMany(c => c.Comments)
                .HasForeignKey(b => b.MovieId);

            modelBuilder.Entity<Comment>()
                .HasOne<ApplicationUser>(o => o.AppUser)
                .WithMany()
                .HasForeignKey(b => b.userId);


        }
    }
}