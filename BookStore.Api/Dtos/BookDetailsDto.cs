namespace BookStore.Api.DetailDtos;

public record BookDetailsDto(
    int Id,
    string Name,
    int AuthorID,
    decimal Price,
    DateOnly ReleaseDate
);
