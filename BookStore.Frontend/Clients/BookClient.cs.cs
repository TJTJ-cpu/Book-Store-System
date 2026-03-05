using System.Net.Http.Json;
using BookStore.Frontend.Dtos;

namespace BookStore.Frontend.Clients;

public class BookClient
{
    private readonly HttpClient _httpClient;

    public BookClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BookSummaryDto[]> GetBooksAsync()
    {
        var books = await _httpClient.GetFromJsonAsync<BookSummaryDto[]>("books");
        
        return books ?? []; 
    }

    // Add book func
    public async Task AddBookAsync(CreateBookDto newBook)
    {
        await _httpClient.PostAsJsonAsync("books", newBook);
    }

    // Delete book fun
    public async Task DeleteBookAsync(int id)
    {
        await _httpClient.DeleteAsync($"books/{id}");
    }

}