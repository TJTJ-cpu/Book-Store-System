using System.Net.Http.Json;
using System.Text.Json;
using BookStore.Frontend.Dtos;

namespace BookStore.Frontend.Clients;

public class BookClient
{
    private readonly HttpClient mHttpClient;
    private record ValidationError(string Title, Dictionary<string, string[]> Errors);


    public BookClient(HttpClient httpClient)
    {
        mHttpClient = httpClient;
    }

    public async Task<BookSummaryDto[]> GetBooksAsync()
    {
        var books = await mHttpClient.GetFromJsonAsync<BookSummaryDto[]>("books");
        
        return books ?? []; 
    }

    // Add book func
    public async Task<string?> AddBookAsync(CreateBookDto newBook)
    {
        var response = await mHttpClient.PostAsJsonAsync("books", newBook);
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
        try
        {
            var err = JsonSerializer.Deserialize<ValidationError>(content, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (err?.Errors != null)
                return string.Join(" | ", err.Errors.SelectMany(e => e.Value));
        }
        catch { }
        return content;


        }
        return null;
    }

    public async Task DeleteBookAsync(int id)
    {
        await mHttpClient.DeleteAsync($"books/{id}");
    }

}