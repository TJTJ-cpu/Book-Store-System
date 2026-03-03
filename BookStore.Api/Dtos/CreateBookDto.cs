using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Dtos;

public record CreateBookDto (
    [Required][StringLength(50)] string Name,
    [Range(1, int.MaxValue)] int AuthorId,
    [Range(0, 100)] decimal Price,
    DateOnly ReleaseDate
);
