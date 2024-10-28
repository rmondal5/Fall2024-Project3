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
                    Name = "Natalie Portman",
                    Gender = "Female",
                    Age = 40,
                    IMDBLink = "https://www.imdb.com/name/nm0000204/",
                    Photo = "https://assets.vogue.com/photos/65e20003a49bdf770fdb9883/master/w_1920,c_limit/F001%20MD%20PARFUM%2024%20BTS%2013E_L4_RVB.jpg"
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
                    Name = "Scarlett Johansson",
                    Gender = "Female",
                    Age = 36,
                    IMDBLink = "https://www.imdb.com/name/nm0424060/",
                    Photo = "https://www.usmagazine.com/wp-content/uploads/2023/09/Scarlett-Johansson-Emily-Ratajkowski-Says-Its-%E2%80%98Chic-to-Get-Divorced-Before-30-Here-are-10-Stars-Who-Qualify-.jpg?w=800&h=1421&crop=1&quality=40&strip=all.jpg"
                },
                new Actor
                {
                    Id = 6,
                    Name = "Tom Hanks",
                    Gender = "Male",
                    Age = 65,
                    IMDBLink = "https://www.imdb.com/name/nm0000158/",
                    Photo = "https://www.usmagazine.com/wp-content/uploads/2023/05/Tom-Hanks-Thinks-He-Could-Star-in-Movies-Posthumously-With-AI-Technology-inline.jpg?w=800&h=1421&crop=1&quality=86&strip=all.jpg"
                },
                new Actor
                {
                    Id = 7,
                    Name = "Meryl Streep",
                    Gender = "Female",
                    Age = 72,
                    IMDBLink = "https://www.imdb.com/name/nm0000658/",
                    Photo = "https://people.com/thmb/TWCf8YJqiXi2tEVM4EqlS9pLN_c=/750x0/filters:no_upscale():max_bytes(150000):strip_icc():focal(2999x0:3001x2):format(webp)/peo-meryl-streep-tie-neck-blouse-tout-e83e0318d7ad4dc2bbce869c5deb852a.jpg"
                },
                new Actor
                {
                    Id = 8,
                    Name = "Robert Downey Jr.",
                    Gender = "Male",
                    Age = 56,
                    IMDBLink = "https://www.imdb.com/name/nm0000375/",
                    Photo = "https://media-cldnry.s-nbcnews.com/image/upload/t_fit-1000w,f_auto,q_auto:best/rockcms/2023-01/robert-downey-jr-2-te-230112-5af7b1.jpg"
                }
            );

            // Add sample MovieActor relationships
            modelBuilder.Entity<MovieActor>().HasData(
                new MovieActor { MovieId = 1, ActorId = 1 }, // Leonardo DiCaprio in Inception
                new MovieActor { MovieId = 2, ActorId = 2 }, // Natalie Portman in The Matrix
                new MovieActor { MovieId = 3, ActorId = 3 }, // Morgan Freeman in The Shawshank Redemption
                new MovieActor { MovieId = 4, ActorId = 4 }  // Samuel L. Jackson in Pulp Fiction
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}