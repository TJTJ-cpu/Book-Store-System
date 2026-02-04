using BookStore.Api.Data;
using BookStore.Api.DetailDtos;
using BookStore.Api.Dtos;
using BookStore.Api.Models;

namespace BookStore.Api.EndPoints;

public static class BookEndPoints
{
    const string GetEndPointBook = "GetBookRoute";
    private static readonly List<BookDto> books = [
        new (1, "Meditations", "Marcus Aurelius", 9.99M, new DateOnly(1558, 01, 01)),
        new (2, "1984", "George Orwell", 11.99M, new DateOnly(1949, 06, 08)),
        new (3, "Sunrise on the Reaping", "Suzanne Collins", 27.99M, new DateOnly(2025, 03, 18)),
    ];

    public static void MapBookEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/books");

        // GET /book
        group.MapGet("/", () => books);

        // GET /books/id
        group.MapGet("/{id}", (int id) =>
        {
            var book = books.Find(book => book.Id == id);

            return book is null ? Results.NotFound() : Results.Ok(book);
        }).WithName(GetEndPointBook);

        // POST /book
        group.MapPost("/", (CreateBookDto newBook, BookStoreContext dbContext) =>
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
            dbContext.SaveChanges();

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
        group.MapPut("/{id}", (int id, UpdateBookDto updatedBook) =>
        {
            var index = books.FindIndex(book => book.Id == id);

            if (index == -1)
                Results.NotFound();

            books[index] = new BookDto(
                id,
                updatedBook.Name,
                updatedBook.Author,
                updatedBook.Price,
                updatedBook.ReleaseDate
            );
            return Results.NoContent();
        });

        // DELETE /books/id
        group.MapDelete("/{id}", (int id) =>
        {
            var index = books.FindIndex(book => book.Id == id);
            if (index == -1)
                return Results.NotFound();

            books.RemoveAll(book => book.Id == id);

            return Results.NoContent();
        });

    }

}
