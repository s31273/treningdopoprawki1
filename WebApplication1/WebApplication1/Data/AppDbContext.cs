using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{
    public DbSet<Reader> Readers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<ReaderBook> ReaderBooks { get; set; }
    public DbSet<Borrowing> Borrowings { get; set; }


    public AppDbContext(DbContextOptions options) : base(options)
    {

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var readers = new List<Reader>
        {
            new()
            {
                Id = 1,
                Name = "Anna Nowwak",
                Email = "anna.nowak@example.edu"
            },
            new()
            {
                Id = 2,
                Name = "Anna Kowalska",
                Email = "anna.kowalska@example.edu"
            }
        };

        var books = new List<Book>
        {
            new()
            {
                Id = 101,
                Title = "The Hobbit",
                Genre = "Fantasy"
            },
            new()
            {
                Id = 102,
                Title = "Harry Potter",
                Genre = "Fantasy"
            },
            new()
            {
                Id = 103,
                Title = "Sapiens",
                Genre = "History"
            }
        };

        var borrowings = new List<Borrowing>
        {
            new()
            {
                BookId = 101,
                ReaderId = 1,
                BorrowDate = new DateTime(2025, 6, 18, 18, 30, 0),
                ReturnStatus = "Borrowed"
            },
            new()
            {
                BookId = 102,
                ReaderId = 2,
                BorrowDate = new DateTime(2025, 5, 13, 12, 30, 0),
                ReturnStatus = "Borrowed"
            },
            new()
            {
                BookId = 103,
                ReaderId = 1,
                BorrowDate = new DateTime(2025, 5, 20, 17, 30, 5),
                ReturnStatus = "Borrowed"
            }
        };
        var ReaderBooks = new List<ReaderBook>
        {
            new()
            {
                BookId = 101,
                ReaderId = 1
            },
            new()
            {
                BookId = 102,
                ReaderId = 2
            },
            new()
            {
                BookId = 103,
                ReaderId = 1
            }
        };

        modelBuilder.Entity<Reader>().HasData(readers);
        modelBuilder.Entity<Book>().HasData(books);
        modelBuilder.Entity<Borrowing>().HasData(borrowings);
        modelBuilder.Entity<ReaderBook>().HasData(ReaderBooks);
    }
}
