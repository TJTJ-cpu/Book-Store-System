namespace BookStore.Api.Dtos;

public record CreateBookDto (
    string Name,
    string Author,
    decimal Price,
    DateOnly ReleaseDate
);
