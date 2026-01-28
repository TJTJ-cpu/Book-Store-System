using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using BookStore.Api.Dtos;
using BookStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var app = builder.Build();


app.MapBookEndPoints();

app.Run();
