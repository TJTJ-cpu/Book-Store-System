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
}