using BookStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<BookStoreContext>();

        // Dereference of a possibly null reference.
        #pragma warning disable CS8602 
        dbContext.Database.Migrate();
        // Dereference of a possibly null reference.
        #pragma warning restore CS8602 
    }

    public static void AddBookStoreDb(this WebApplicationBuilder builder)
    {
        var conString = builder.Configuration.GetConnectionString("BookStore");
        builder.Services.AddScoped<BookStoreContext>();
        builder.Services.AddSqlite<BookStoreContext>(
            conString, 
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<Author>().Any())
                {
                    context.Set<Author>().AddRange(
                        new Author { Name = "TJ Mady" },
                        new Author { Name = "Stephen R. Covey" },
                        new Author { Name = "Viktor E. Frankl" },
                        new Author { Name = "Marcus Aurelius" },
                        new Author { Name = "Jordan Peterson" },
                        new Author { Name = "Tim Ferriss" },
                        new Author { Name = "David Goggins" },
                        new Author { Name = "Mel Robbins" },
                        new Author { Name = "Robert Cialdini" },
                        new Author { Name = "Ryan Holiday" },
                        new Author { Name = "Robin Sharma" }

                    );
                    context.SaveChanges();
                }
            })
);

    }

}
