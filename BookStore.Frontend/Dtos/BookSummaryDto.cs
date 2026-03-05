namespace BookStore.Frontend.Dtos;

public record BookSummaryDto(
    int Id,
    string Name,
    string Author,
    decimal Price,
    DateOnly ReleaseDate
);
