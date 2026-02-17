# Ticket Support System API

A B2B Ticket Support System Backend built with **.NET 8**, adhering to **Clean Architecture** and **SOLID** principles.

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Docker & Docker Compose
- SQL Server (via Docker)

### Installation

1. **Clone and Setup**
   ```bash
   git clone https://github.com/your-username/ticket-support.git
   cd ticket-support
   ```

2. **Option A: Run with Docker (Recommended for Prod/DevOps)**
   Ensure Docker Desktop is running.
   ```bash
   docker-compose up -d
   ```

2. **Option B: Run Locally (Fastest for Dev)**
   If Docker is not available, the app is configured to use **LocalDB** automatically.
   ```bash
   # Create LocalDB instance (one time)
   sqllocaldb create TicketSupportDb
   ```

3. **Database Migration**
   Apply the migrations to create the database schema (works for both Docker and LocalDB):
   ```bash
   dotnet ef database update --project src/TicketSupport.Infrastructure --startup-project src/TicketSupport.API
   ```

4. **Run the API**
   ```bash
   cd src/TicketSupport.API
   dotnet run
   ```
   Access Swagger UI at: `http://localhost:5000/swagger`

## 🏗 Architecture

The project follows strict **Clean Architecture** with 4 layers:
- **Domain**: Core entities (`Ticket`, `User`, `Tenant`) and logic. Zero dependencies.
- **Application**: Business logic (`TicketService`), DTOs, Validators (`FluentValidation`), and Interfaces.
- **Infrastructure**: Implementation details (`AppDbContext`, `TicketRepository`). Depends on Application & Domain.
- **API**: Entry point, Controllers, DI configuration.

### Design Decisions & Trade-offs
- **Repository Pattern**: Used to decouple Application from EF Core, ensuring `TicketService` doesn't depend on `DbContext` directly (adhering to D.I.P.).
- **Async/Await**: Used consistently for I/O operations to maximize throughput.
- **SQL Server**: Chosen for robust relational data integrity (FKs, Transactions).
- **Testability**: Architecture allows swapping Infrastructure (e.g., Use In-Memory DB for tests).

## 🧪 Testing

Run unit tests:
```bash
dotnet test
```

## 🛠 Tech Stack
- **.NET 8** (C#)
- **Entity Framework Core 8** (ORM)
- **SQL Server** (Database)
- **Serilog** (Logging)
- **FluentValidation** (Validation)
- **Swagger/OpenAPI** (Documentation)
- **Docker** (Containerization)

## 📌 Key Endpoints
- `GET /api/tickets?tenantId={guid}` - List tickets
- `POST /api/tickets` - Create ticket
- `GET /api/tickets/{id}` - Get details
