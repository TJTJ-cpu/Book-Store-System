namespace BookStore.Api.Dtos;

public record UpdateBookDto(
    string Name,
    string Author,
    decimal Price,
    DateOnly ReleaseDate
);
