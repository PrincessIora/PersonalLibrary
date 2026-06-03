📚 Personal Library API

A lightweight ASP.NET Core Web API for tracking personal reading habits, managing books, and monitoring reading progress over time.

This project demonstrates software engineering principles including state machines, layered architecture, RESTful design, and automated testing.

✨ Features
📖 Add and manage books
🔁 Strict book state transitions:
WantToRead → Reading → Finished → Reading
🔎 Search by title or status
📊 Reading history tracking
🔥 Reading streak calculation
⭐ Optional rating, description, and publication year
🧪 Unit + integration test coverage
📘 Swagger UI with custom themed interface
🏗 Architecture

The system follows a layered architecture:

Controllers → Services → Repositories → Database
Layers
Controllers → API endpoints
Services → Business logic & state machine rules
Repositories → Data access abstraction
Data (EF Core) → SQLite persistence layer
📦 Data Model
Book
Id
Title
Author
Status (WantToRead, Reading, Finished)
Description (optional)
Rating (optional, 1–5)
Year (optional)
ReadingHistory
Id
BookId
Status
Date
🔁 State Machine Rules

Books follow a strict workflow:

WantToRead → Reading → Finished → Reading
Rules
Max 3 books can be in Reading state at once
Invalid transitions are rejected
All valid transitions are logged in history
🔌 API Endpoints
📘 Create Book
POST /books
Body
{
  "title": "The Hobbit",
  "author": "J.R.R. Tolkien"
}
✏️ Update Book
PUT /books/{id}
Body
{
  "status": "Reading",
  "description": "optional",
  "rating": 4.5,
  "year": 1937
}
🔎 Search Books
GET /books/search?title=hobbit&status=Finished
📜 Get Reading History
GET /books/{id}/history
🔥 Get Reading Streak
GET /books/{id}/streak?date=2026-06-03
🧪 Testing

The project includes:

✔ Unit Tests
Book creation
Search filtering
State transitions
✔ Boundary Tests
Rating validation
Reading limit enforcement
✔ Integration Tests
History tracking
Service + repository interaction
✔ System Tests
Full API → DB persistence flow

Run tests:

dotnet test
🎨 Swagger UI Theme

Swagger UI is customized with a soft pink “royal library” theme.

Features:
Pink UI styling
Custom header branding
Clean model display
Improved readability

Access:

https://localhost:65510/swagger
⚙️ Tech Stack
ASP.NET Core Web API
Entity Framework Core
SQLite
Swagger / OpenAPI
xUnit + FluentAssertions
🚀 Running the Project
1. Restore dependencies
dotnet restore
2. Run database migrations (if needed)
dotnet ef database update
3. Run application
dotnet run
4. Open Swagger
https://localhost:65510/swagger
🧠 Key Design Decisions
1. Layered Architecture

Separation of concerns between API, business logic, and data access improves maintainability and testability.

2. Enum-based State Machine

Strongly typed BookStatus ensures invalid states cannot be stored in the system.

🔐 Configuration & Secrets
SQLite connection string stored in appsettings.json
No hardcoded secrets
Designed for easy migration to cloud environments
📊 Logging Strategy

The system logs:

State transitions
Invalid updates
Reading history changes
API execution flow

These logs help debug:

invalid state transitions
streak calculation issues
data consistency problems
🔄 Future Improvements
User authentication (multi-user support)
Pagination for large libraries
Advanced analytics (reading speed, genre tracking)
Cloud deployment (Azure/AWS)
