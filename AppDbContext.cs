using BookingMangementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingMangementSystem.Services
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                    : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var user = new IdentityRole("user");
            user.NormalizedName = "user";

            builder.Entity<IdentityRole>().HasData(user, admin);

            // Seed authors with real names
            builder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "J.K. Rowling" },
                new Author { Id = 2, Name = "George R.R. Martin" },
                new Author { Id = 3, Name = "J.R.R. Tolkien" },
                new Author { Id = 4, Name = "Agatha Christie" },
                new Author { Id = 5, Name = "Stephen King" },
                new Author { Id = 6, Name = "Mark Twain" },
                new Author { Id = 7, Name = "Jane Austen" },
                new Author { Id = 8, Name = "Ernest Hemingway" },
                new Author { Id = 9, Name = "F. Scott Fitzgerald" },
                new Author { Id = 10, Name = "Harper Lee" }
            );
        }
    }
}