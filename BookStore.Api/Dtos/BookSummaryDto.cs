namespace BookStore.Api.Dtos;

public record BookSummaryDto(
    int Id,
    string Name,
    string Author,
    decimal Price,
    DateOnly ReleaseDate
);
