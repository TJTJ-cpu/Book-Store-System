using System.Runtime.InteropServices;
using BookStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Data;

// This file is the bridge between the C# code and the Data
public class BookStoreContext(DbContextOptions<BookStoreContext> options) 
    : DbContext(options)
{

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();

}
