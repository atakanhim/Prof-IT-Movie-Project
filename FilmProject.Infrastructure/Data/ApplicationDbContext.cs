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

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MovieCategoryMap> MovieCategoryMaps { get; set; }
        public DbSet<Favorite> Favorite { get; set; }

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
              .WithMany(b => b.MovieCategories)
              .HasForeignKey(bc => bc.CategoryId);


            modelBuilder.Entity<Favorite>()
                        .HasKey(bc => new { bc.MovieId, bc.UserId });

            modelBuilder.Entity<Favorite>()
                .HasOne(bc => bc.Movie)
                .WithMany(b => b.WhoFavorited)
                .HasForeignKey(bc => bc.MovieId);

            modelBuilder.Entity<Favorite>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.Favorite)
                .HasForeignKey(bc => bc.UserId);


        }
    }
}