using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Dtos;

public record CreateBookDto (
    [Required][StringLength(50)] string Name,
    [Required][StringLength(20)] string Author,
    [Range(0, 100)] decimal Price,
    DateOnly ReleaseDate
);
