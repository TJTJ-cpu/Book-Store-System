namespace BookStore.Frontend.Dtos;

public class UpdateBookDto
{
    public string Name { get; set; } = "";
    public int AuthorId { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}

