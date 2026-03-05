using System.Net.Http.Json;
using BookStore.Frontend.Dtos;

namespace BookStore.Frontend.Clients;

public class BookClient
{
    private readonly HttpClient mHttpClient;

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
        var respnse = await mHttpClient.PostAsJsonAsync("books", newBook);
        if (!respnse.IsSuccessStatusCode)
            return await respnse.Content.ReadAsStringAsync();
        return null;
    }

    // Delete book fun
    public async Task DeleteBookAsync(int id)
    {
        await mHttpClient.DeleteAsync($"books/{id}");
    }

}