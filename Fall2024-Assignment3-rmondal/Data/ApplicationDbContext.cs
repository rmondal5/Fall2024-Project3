using Microsoft.EntityFrameworkCore;
using Fall2024_Assignment3_rmondal.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<MovieActor> MovieActors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the MovieActor relationship
        modelBuilder.Entity<MovieActor>()
            .HasKey(ma => new { ma.MovieId, ma.ActorId });

        modelBuilder.Entity<MovieActor>()
            .HasOne(ma => ma.Movie)
            .WithMany(m => m.MovieActors)
            .HasForeignKey(ma => ma.MovieId);

        modelBuilder.Entity<MovieActor>()
            .HasOne(ma => ma.Actor)
            .WithMany(a => a.MovieActors)
            .HasForeignKey(ma => ma.ActorId);

        // Seed movies with required IMDBLink
        modelBuilder.Entity<Movie>().HasData(
            new Movie
            {
                Id = 1,
                Title = "Inception",
                Genre = "Sci-Fi",
                Year = 2010,
                IMDBLink = "https://www.imdb.com/title/tt1375666/"
            },
            new Movie
            {
                Id = 2,
                Title = "The Matrix",
                Genre = "Action",
                Year = 1999,
                IMDBLink = "https://www.imdb.com/title/tt0133093/"
            }
        );

        // Call base.OnModelCreating to ensure any EF Core-specific configurations are applied
        base.OnModelCreating(modelBuilder);
    }
}
