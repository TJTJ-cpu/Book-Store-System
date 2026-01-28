using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using BookStore.Api.Dtos;
using BookStore.Api.EndPoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapBookEndPoints();

app.Run();
