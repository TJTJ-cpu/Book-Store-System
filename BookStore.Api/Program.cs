using BookStore.Api.Data;
using BookStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.AddBookStoreDb();

var app = builder.Build();

app.MapBookEndPoints();

app.MigrateDb();

app.Run();
