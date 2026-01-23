namespace BookStore.Api.Dtos;

public record BookDto(
    int Id,
    string Name,
    string Author,
    decimal Price,
    DateOnly ReleaseDate
);
