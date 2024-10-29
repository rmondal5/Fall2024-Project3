using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Fall2024_Assignment3_rmondal.Models;

namespace Fall2024_Assignment3_rmondal.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
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

            // Seed movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Inception",
                    Genre = "Sci-Fi",
                    Year = 2010,
                    IMDBLink = "https://www.imdb.com/title/tt1375666/",
                    Poster = "https://i.ebayimg.com/images/g/f6EAAOSwk5FUwoOs/s-l1600.jpg"
                },
                new Movie
                {
                    Id = 2,
                    Title = "The Matrix",
                    Genre = "Action",
                    Year = 1999,
                    IMDBLink = "https://www.imdb.com/title/tt0133093/",
                    Poster = "https://i.ebayimg.com/images/g/tD4AAOSw31JfmYLd/s-l1600.jpg"
                },
                new Movie
                {
                    Id = 3,
                    Title = "The Shawshank Redemption",
                    Genre = "Drama",
                    Year = 1994,
                    IMDBLink = "https://www.imdb.com/title/tt0111161/",
                    Poster = "https://www.originalfilmart.com/cdn/shop/products/shawshank_redemption_1994_netherlands_original_film_art_5000x.jpg?v=1572559869.jpg"
                },
                new Movie
                {
                    Id = 4,
                    Title = "Pulp Fiction",
                    Genre = "Crime",
                    Year = 1994,
                    IMDBLink = "https://www.imdb.com/title/tt0110912/",
                    Poster = "https://m.media-amazon.com/images/I/71wPS3A1EYL._AC_UF1000,1000_QL80_.jpg"
                }
            );

            // Seed actors
            modelBuilder.Entity<Actor>().HasData(
                new Actor
                {
                    Id = 1,
                    Name = "Leonardo DiCaprio",
                    Gender = "Male",
                    Age = 46,
                    IMDBLink = "https://www.imdb.com/name/nm0000138/",
                    Photo = "https://media-cldnry.s-nbcnews.com/image/upload/t_fit-1000w,f_auto,q_auto:best/rockcms/2024-01/leonardo-dicaprio-dating-history-zz-240105-44b86e.jpg"
                },
                new Actor
                {
                    Id = 2,
                    Name = "Keanu Reeves",
                    Gender = "Male",
                    Age = 60,
                    IMDBLink = "https://www.imdb.com/name/nm0000206/",
                    Photo = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcS9Vxd2hrHwjpVZZ8aPB-W-r2dcL665QxjN2nByzfLIOrxmcUSS"
                },
                new Actor
                {
                    Id = 3,
                    Name = "Morgan Freeman",
                    Gender = "Male",
                    Age = 84,
                    IMDBLink = "https://www.imdb.com/name/nm0000151/",
                    Photo = "https://resizing.flixster.com/xtnsozkTuUtYkGMETMLMJfgNTjs=/218x280/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/47162_v9_bc.jpg"
                },
                new Actor
                {
                    Id = 4,
                    Name = "Samuel L. Jackson",
                    Gender = "Male",
                    Age = 72,
                    IMDBLink = "https://www.imdb.com/name/nm0000168/",
                    Photo = "https://hips.hearstapps.com/hmg-prod/images/gettyimages-648731684.jpg?resize=1200:*.jpg"
                },
                new Actor
                {
                    Id = 5,
                    Name = "Carrie-Anne Moss",
                    Gender = "Female",
                    Age = 57,
                    IMDBLink = "https://www.imdb.com/name/nm0005251/",
                    Photo = "https://m.media-amazon.com/images/M/MV5BMTYxMjgwNzEwOF5BMl5BanBnXkFtZTcwNTQ0NzI5Ng@@._V1_.jpg"
                },
                new Actor
                {
                    Id = 6,
                    Name = "Cillian Murphy",
                    Gender = "Male",
                    Age = 48,
                    IMDBLink = "https://www.imdb.com/name/nm0614165/",
                    Photo = "https://resizing.flixster.com/EYqSJyq1ZhI9IJ16Wpv_WwlroEY=/fit-in/705x460/v2/https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/236083_v9_bd.jpg"
                },
                new Actor
                {
                    Id = 7,
                    Name = "Tim Robbins",
                    Gender = "Male",
                    Age = 66,
                    IMDBLink = "https://www.imdb.com/name/nm0000209/",
                    Photo = "https://resizing.flixster.com/-XZAfHZM39UwaGJIFWKAE8fS0ak=/v3/t/assets/1473_v9_bc.jpg"
                },
                new Actor
                {
                    Id = 8,
                    Name = "Uma Thurman",
                    Gender = "Female",
                    Age = 55,
                    IMDBLink = "https://www.imdb.com/name/nm0000235/",
                    Photo = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT4Cd8BrnUS8RuD1DFuhr_4uVGx4yFHgmQXAath2gBjDK3rNyIv"
                }
            );

            // Add sample MovieActor relationships
            modelBuilder.Entity<MovieActor>().HasData(
                new MovieActor { MovieId = 1, ActorId = 1 }, // Leonardo DiCaprio in Inception
                new MovieActor { MovieId = 1, ActorId = 6 },
                new MovieActor { MovieId = 2, ActorId = 2 }, // Natalie Portman in The Matrix
                new MovieActor { MovieId = 2, ActorId = 5 },
                new MovieActor { MovieId = 3, ActorId = 3 }, // Morgan Freeman in The Shawshank Redemption
                new MovieActor { MovieId = 3, ActorId = 7 },
                new MovieActor { MovieId = 4, ActorId = 4 },  // Samuel L. Jackson in Pulp Fiction
                new MovieActor { MovieId = 4, ActorId = 8 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}