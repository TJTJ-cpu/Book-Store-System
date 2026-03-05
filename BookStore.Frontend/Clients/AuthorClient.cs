using System;
using System.Net.Http.Json;
using BookStore.Frontend.Dtos;
namespace BookStore.Frontend.Clients;

public class AuthorClient
{
    private readonly HttpClient mHttpclient;
    public AuthorClient(HttpClient httpClient)
    {
        mHttpclient = httpClient;
    }

    public async Task<AuthorDto[]> GetAuthorsAsync()
    {
        var authors = await mHttpclient.GetFromJsonAsync<AuthorDto[]>("author");
        return authors ?? [];
    }

    public async Task<string?> AddAuthorAsync(string name)
    {
        var response = await mHttpclient.PostAsJsonAsync("author", new {Name = name});
        if (!response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync();
        return null;

    }

}