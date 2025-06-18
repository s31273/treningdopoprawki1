using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;


namespace WebApplication4.Service;

public interface IDbService
{
    public Task <ICollection<GetReaderDto>> GetAllReadersAsync();
    public Task<GetReaderDto> CreateReaderAsync(CreateReaderDto createReaderDto);
    public Task DeleteReaderAsync(int readerId); 
    public Task<GetReaderDto> UpdateReaderAsync(UpdateReaderDto updateReaderDto, int readerId);
}

public class DbService(AppDbContext data) : IDbService
{
    public async Task<ICollection<GetReaderDto>> GetAllReadersAsync()
    {
        return await data.Readers.Select(rd => new GetReaderDto
        {
            Id = rd.Id,
            Name = rd.Name,
            Email = rd.Email,
            Books = rd.Borrowings.Select(ga => new GetBookDetailsForReaderDto()
            {
                Title = ga.Book.Title,
                Genre = ga.Book.Genre,
                BorrowDate = ga.BorrowDate,
                ReturnStatus = ga.ReturnStatus
            }).ToList()
        }).ToListAsync();
    }


    public async Task<GetReaderDto> CreateReaderAsync(CreateReaderDto createReaderDto)
    {
        var existingReader = await data.Readers.FirstOrDefaultAsync(r => r.Name == createReaderDto.Name && r.Email == createReaderDto.Email);
        if (existingReader != null)
        {
            throw new NotFoundException("Taki uzytkownik juz istnieje.");
        }

        var booksEntities = new List<Book>();
        await using var transaction = await data.Database.BeginTransactionAsync();
        try
        {
            foreach (var b in createReaderDto.Books)
            {
                var book = await data.Books.FirstOrDefaultAsync(a => a.Title == b.Title && a.Genre == b.Genre);
                if (book == null)
                {
                    book = new Book()
                    {
                        Title = b.Title,
                        Genre = b.Genre
                    };
                    await data.Books.AddAsync(book);
                }

                booksEntities.Add(book);
            }

            var reader = new Reader()
            {
                Name = createReaderDto.Name,
                Email = createReaderDto.Email
            };
            await data.Readers.AddAsync(reader);
            foreach (var bc in booksEntities)
            {
                var borrowing = new Borrowing()
                {
                    Book = bc,
                    Reader = reader,
                    BorrowDate = DateTime.Now,
                    ReturnStatus = "Borrowed"
                };
                await data.Borrowings.AddAsync(borrowing);
                var readerBook = new ReaderBook()
                {
                    Reader = reader,
                    Book = bc
                };
                await data.ReaderBooks.AddAsync(readerBook);
            }
            await data.SaveChangesAsync();
            await transaction.CommitAsync();

            return new GetReaderDto()
            {
                Id = reader.Id,
                Name = reader.Name,
                Email = reader.Email,
                Books = reader.Borrowings.Select(ga => new GetBookDetailsForReaderDto()
                {
                    Title = ga.Book.Title,
                    Genre = ga.Book.Genre,
                    BorrowDate = ga.BorrowDate,
                    ReturnStatus = ga.ReturnStatus
                }).ToList()
            };
        }
        catch(Exception)
        {
            await transaction.RollbackAsync();
            throw;
        } 
    }

    public async Task DeleteReaderAsync(int readerId)
    {
        var reader = await data.Readers.FirstOrDefaultAsync(r => r.Id == readerId);
        if (reader == null)
        {
            throw new NotFoundException("Taki uzytkownik nie istnieje.");
        }
        var borrowings = await data.Borrowings.Where(b => b.ReaderId == readerId).ToListAsync();
        var readerBooks = await data.ReaderBooks.Where(b => b.ReaderId == readerId).ToListAsync();
        data.ReaderBooks.RemoveRange(readerBooks);
        data.Borrowings.RemoveRange(borrowings);
        data.Readers.Remove(reader);
        data.SaveChanges();
    }
    
    
   public async Task<GetReaderDto> UpdateReaderAsync(UpdateReaderDto updateReaderDto, int readerId)
{
    using var transaction = await data.Database.BeginTransactionAsync();

    try
    {
        var reader = await data.Readers.FirstOrDefaultAsync(r => r.Id == readerId);
        if (reader == null)
            throw new NotFoundException("Taki uzytkownik nie istnieje.");

        reader.Name = updateReaderDto.Name;
        reader.Email = updateReaderDto.Email;

        var borrowings = await data.Borrowings.Where(b => b.ReaderId == readerId).ToListAsync();
        var readerBooks = await data.ReaderBooks.Where(b => b.ReaderId == readerId).ToListAsync();

        data.Borrowings.RemoveRange(borrowings);
        data.ReaderBooks.RemoveRange(readerBooks);

        foreach (var borrowing in updateReaderDto.Borrowings)
        {
            var existingBook = await data.Books.FirstOrDefaultAsync(b => b.Title == borrowing.Title);

            if (existingBook == null)
            {
                existingBook = new Book
                {
                    Title = borrowing.Title,
                    Genre = borrowing.Genre
                };
                data.Books.Add(existingBook);
                await data.SaveChangesAsync(); 
            }

            data.ReaderBooks.Add(new ReaderBook
            {
                Reader = reader,
                Book = existingBook
            });

            data.Borrowings.Add(new Borrowing
            {
                Reader = reader,
                Book = existingBook,
                BorrowDate = borrowing.BorrowDate,
                ReturnStatus = borrowing.ReturnStatus
            });
        }

        await data.SaveChangesAsync();
        await transaction.CommitAsync();

        return new GetReaderDto
        {
            Id = reader.Id,
            Name = reader.Name,
            Email = reader.Email,
            Books = reader.Borrowings.Select(ga => new GetBookDetailsForReaderDto
            {
                Title = ga.Book.Title,
                Genre = ga.Book.Genre,
                BorrowDate = ga.BorrowDate,
                ReturnStatus = ga.ReturnStatus
            }).ToList()
        };
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }
}

    
    
}



