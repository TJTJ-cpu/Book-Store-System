# Book Store System

A full-stack book store management application built with **.NET 10 Minimal API** and **Blazor WebAssembly**.

## Tech Stack

| Layer | Technology |
|---|---|
| Backend | .NET 10 Minimal API (C#) |
| Frontend | Blazor WebAssembly |
| Database | PostgreSQL + Entity Framework Core |
| Styling | Bootstrap 5 + Bootstrap Icons |

## Features

- View all books with author, price, and release date
- Add a new book with author dropdown selection and price validation
- Delete a book
- View all authors
- Add a new author

## API Endpoints

### Books
| Method | Route | Description |
|---|---|---|
| GET | `/books` | Get all books |
| GET | `/books/{id}` | Get book by ID |
| POST | `/books` | Add a new book |
| PUT | `/books/{id}` | Update a book |
| DELETE | `/books/{id}` | Delete a book |

### Authors
| Method | Route | Description |
|---|---|---|
| GET | `/author` | Get all authors |
| POST | `/author` | Add a new author |
| DELETE | `/author/{id}` | Delete an author |

## Getting Started

### Prerequisites
- .NET 10 SDK
- PostgreSQL

### Setup

1. Clone the repository
```
git clone https://github.com/your-username/Book-Store-System.git
```

2. Update the connection string in `BookStore.Api/appsettings.json`:
```json
"ConnectionStrings": {
  "BookStoreDb": "Host=localhost;Database=BookStore;Username=youruser;Password=yourpassword"
}
```

3. Run the API (port 5204):
```
cd BookStore.Api
dotnet run
```

4. Run the frontend (port 5100):
```
cd BookStore.Frontend
dotnet run
```

5. Open your browser at `http://localhost:5100`
