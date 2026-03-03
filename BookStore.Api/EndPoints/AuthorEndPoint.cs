
using System.Runtime.CompilerServices;
using BookStore.Api.Data;
using BookStore.Api.Dtos;
using BookStore.Api.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

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

        // POST
        group.MapPost("/", async (CreateAuthorDto newAuthor, BookStoreContext dbContext) =>
        {
            if (string.IsNullOrEmpty(newAuthor.Name))
                return Results.BadRequest("Author name cannot be empty.");

            var existedAuthor = await dbContext.Authors.AnyAsync(a => a.Name == newAuthor.Name);
            if (existedAuthor)
                return Results.Conflict($"The author {newAuthor.Name} is already in the database.");

            var author = new Author
            {
                Name = newAuthor.Name
            };
            
            dbContext.Add(author);
            await dbContext.SaveChangesAsync();
            return Results.Ok(new AuthorDto(author.Id, author.Name));
        });

        //Delete
        group.MapDelete("/{id}", async (int id, BookStoreContext dbContext) =>
        {
            var author = await dbContext.Authors.FindAsync(id);

            if (author is null)
                return Results.NotFound();

            dbContext.Authors.Remove(author);
            await dbContext.SaveChangesAsync();
                        
            return Results.Ok(new AuthorDto(author.Id, author.Name));
        });

        // DELETE /author/wipe (Temporary endpoint to reset the database)
        group.MapDelete("/wipe", async (BookStoreContext dbContext) =>
        {
            await dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"Authors\" RESTART IDENTITY CASCADE;");
            
            return Results.NoContent();
        });
    }
}
