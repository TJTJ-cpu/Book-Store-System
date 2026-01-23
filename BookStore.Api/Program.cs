using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using BookStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetEndPointBook = "GetBookRoute";

List<BookDto> books = [
    new (1, "Meditations", "Marcus Aurelius", 9.99M, new DateOnly(1558, 01, 01)),
    new (2, "1984", "George Orwell", 11.99M, new DateOnly(1949, 06, 08)),
    new (3, "Sunrise on the Reaping", "Suzanne Collins", 27.99M, new DateOnly(2025, 03, 18)),
];

// GET /book
app.MapGet("/books", () => books);

// GET /books/id
app.MapGet("/books/{id}", (int id) => books.Find(book => book.Id == id)).WithName(GetEndPointBook);

// POST /book
app.MapPost("/books", (CreateBookDto newBook) =>
{
    BookDto book = new (
        books.Count + 1,
        newBook.Name,
        newBook.Author,
        newBook.Price,
        newBook.ReleaseDate
    );

    books.Add(book);
    return Results.CreatedAtRoute(GetEndPointBook, new {id = book.Id}, book);
});

app.Run();
