
using BookStore.Api.Data;
using BookStore.Api.Dtos;
using BookStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.EndPoints;

public static class AuthorEndPoints
{
    public static void MapAuthorEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/author");

        // Get
        group.MapGet("/", async (BookStoreContext dbContext) => 
            await dbContext.Authors.Select(
                Author => new AuthorDto(Author.Id, Author.Name)).AsNoTracking().ToListAsync());
    }
}
