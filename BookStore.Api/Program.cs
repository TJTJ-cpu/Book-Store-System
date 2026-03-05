using BookStore.Api.Data;
using BookStore.Api.EndPoints;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("BookStoreDb")));


// old sqlLite system
// builder.AddBookStoreDb();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5100")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();

app.MapBookEndPoints();

app.MapAuthorEndPoints();

app.MigrateDb();

app.Run();
