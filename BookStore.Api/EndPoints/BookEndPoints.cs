using BookStore.Api.Data;
using BookStore.Api.DetailDtos;
using BookStore.Api.Dtos;
using BookStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.EndPoints;

public static class BookEndPoints
{
    const string GetEndPointBook = "GetBookRoute";
    private static readonly List<BookSummaryDto> books = [
        new (1, "Meditations", "Marcus Aurelius", 9.99M, new DateOnly(1558, 01, 01)),
        new (2, "1984", "George Orwell", 11.99M, new DateOnly(1949, 06, 08)),
        new (3, "Sunrise on the Reaping", "Suzanne Collins", 27.99M, new DateOnly(2025, 03, 18)),
    ];

    public static void MapBookEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/books");

        // GET /book
        group.MapGet("/",async (BookStoreContext dbContext) 
            => await dbContext.Books
                .Include(book => book.Author)
                .Select(book => new BookSummaryDto(
                    book.Id,
                    book.Name,
                    book.Author!.Name,
                    book.Price,
                    book.ReleaseDate
            ))
        .AsNoTracking()
        .ToListAsync());

        // GET /books/id
        group.MapGet("/{id}", async (int id, BookStoreContext dbContext) =>
        {
            var book = await dbContext.Books.FindAsync(id);

            return book is null ? Results.NotFound() : Results.Ok(
                new BookDetailsDto(
                    book.Id,
                    book.Name,
                    book.AuthorId,
                    book.Price,
                    book.ReleaseDate
                )
            );
        }).WithName(GetEndPointBook);

        // POST /random book
        group.MapPost("/random", async (BookStoreContext dbContext) =>
        {
            // temp
            var jsonSting = await File.ReadAllTextAsync("Data/books.json");
            var books = System.Text.Json.JsonSerializer.Deserialize<List<CreateBookDto>>(jsonSting);

            if (books is null || books.Count == 0)
                return Results.Ok("Book list is empty!");

            var random = new Random();
            var randomBook = books[random.Next(books.Count)];

            var existedBook = await dbContext.Books.AnyAsync(b => b.Name == randomBook.Name);
            if (existedBook)
                return Results.Conflict($"The book: {randomBook.Name} is already in the list!");

            Book book = new()
            {
                Name = randomBook.Name,
                AuthorId = randomBook.AuthorId,
                Price = randomBook.Price,
                ReleaseDate = randomBook.ReleaseDate,
            };
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();

            BookDetailsDto bookDetail = new(
                book.Id,
                book.Name,
                book.AuthorId,
                book.Price,
                book.ReleaseDate
            );

            return Results.CreatedAtRoute(GetEndPointBook, new {id = bookDetail.Id}, bookDetail);

        });

        // POST /book
        group.MapPost("/",async (CreateBookDto newBook, BookStoreContext dbContext) =>
        {
            if (string.IsNullOrEmpty(newBook.Name))
                return Results.BadRequest("Name is Empty");

            Book book = new()
            {
                Name = newBook.Name,
                AuthorId = newBook.AuthorId,
                Price = newBook.Price,
                ReleaseDate = newBook.ReleaseDate
            };

            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();

            BookDetailsDto bookDto = new(
                book.Id,
                book.Name,
                book.AuthorId,
                book.Price,
                book.ReleaseDate
            );

            return Results.CreatedAtRoute(GetEndPointBook, new {id = bookDto.Id}, bookDto);
        });

        // PUT /books/id
        group.MapPut("/{id}", async (int id, UpdateBookDto updatedBook, BookStoreContext dbContext) =>
        {
            var existingBook = await dbContext.Books.FindAsync(id);
            var authorExist = await dbContext.Authors.AnyAsync(a => a.Id == updatedBook.AuthorId);

            if (existingBook is null)
                return Results.NotFound($"The book with ID: {id} is not found.");

            if (!authorExist)
                return Results.BadRequest($"The Authod ID: {updatedBook.AuthorId} is not found.");

            existingBook.Name = updatedBook.Name;
            existingBook.AuthorId = updatedBook.AuthorId;
            existingBook.Price = updatedBook.Price;
            existingBook.ReleaseDate = updatedBook.ReleaseDate;


            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /books/id
        group.MapDelete("/{id}", async (int id, BookStoreContext dbContext) =>
        {
            await dbContext.Books.Where(book => book.Id == id).ExecuteDeleteAsync();

            return Results.NoContent();
        });

    }

}
