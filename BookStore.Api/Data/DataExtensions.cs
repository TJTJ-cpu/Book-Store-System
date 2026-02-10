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
                            new Author { Name = "Stephen R. Covey" },      // ID 1
                            new Author { Name = "Viktor E. Frankl" },      // ID 2
                            new Author { Name = "Marcus Aurelius" },       // ID 3
                            new Author { Name = "Jordan Peterson" },       // ID 4
                            new Author { Name = "Tim Ferriss" },           // ID 5
                            new Author { Name = "David Goggins" },         // ID 6
                            new Author { Name = "Mel Robbins" },           // ID 7
                            new Author { Name = "Robert Cialdini" },       // ID 8
                            new Author { Name = "Ryan Holiday" },          // ID 9
                            new Author { Name = "Robin Sharma" },          // ID 10

                            new Author { Name = "Tara Westover" },         // ID 11
                            new Author { Name = "Thibaut Meurisse" },      // ID 12
                            new Author { Name = "Ichiro Kishimi" },        // ID 13
                            new Author { Name = "Brianna Wiest" },         // ID 14
                            new Author { Name = "Matthew Hussey" },        // ID 15
                            new Author { Name = "Fumio Sasaki" },          // ID 16
                            new Author { Name = "M. Scott Peck" },         // ID 17
                            new Author { Name = "Noah Kagan" },            // ID 18
                            new Author { Name = "Anthony Youn" },          // ID 19
                            new Author { Name = "Oliver Burkeman" },       // ID 20
                            new Author { Name = "Jia Jiang" },             // ID 21
                            new Author { Name = "Richard Koch" },          // ID 22
                            new Author { Name = "R.B. Sparkman" },         // ID 23
                            new Author { Name = "Eva Illouz" },            // ID 24
                            new Author { Name = "David Deida" },           // ID 25
                            new Author { Name = "Daniel J. Siegel" },      // ID 26
                            new Author { Name = "Henry Cloud" },           // ID 27
                            new Author { Name = "Adam Grant" },            // ID 28
                            new Author { Name = "Thomas Erikson" },        // ID 29
                            new Author { Name = "Paul Millerd" },          // ID 30
                            new Author { Name = "Anna Lembke" }            // ID 31

                    );
                    context.SaveChanges();
                }
            })
);

    }

}
