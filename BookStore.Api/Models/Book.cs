
namespace BookStore.Api.Models;

public class Book
{
    public int Id { get; set;}
    public required string Name { get; set;}
    public Author? Author {get; set;}
    public int AuthorId {get; set;}
    public decimal Price {get; set;}
    public DateOnly ReleaseDate {get; set;}



}
