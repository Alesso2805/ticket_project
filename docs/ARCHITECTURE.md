# Architecture & Data Model

## 1. Project Structure (Deliverable A)

We follow **Clean Architecture** to ensure separation of concerns, testability, and maintainability.

```
TicketSupport/
├── src/
│   ├── TicketSupport.Domain/       # [Core] Entities, Value Objects, Interfaces, Enums
│   │   ├── Entities/               # Ticket, Tenant, User, Comment
│   │   ├── Enums/                  # TicketStatus, TicketPriority, UserRole
│   │   ├── Interfaces/             # IReservationRepository, etc. (if using Repo pattern)
│   │   └── Exceptions/             # Domain Exceptions
│   ├── TicketSupport.Application/  # [Use Cases] Logic orchestration
│   │   ├── DTOs/                   # Data Transfer Objects
│   │   ├── Interfaces/             # ITicketService, IEmailService
│   │   ├── Services/               # TicketService implementation
│   │   ├── Validators/             # FluentValidation rules
│   │   └── Mappings/               # AutoMapper (optional) or manual mapping
│   ├── TicketSupport.Infrastructure/ # [External] DB, FileSystem, 3rd Party APIs
│   │   ├── Data/                   # DbContext, Migrations
│   │   ├── Repositories/           # Repository Implementation (if used)
│   │   └── Services/               # EmailServiceImpl
│   └── TicketSupport.API/          # [Entry Point] Controllers, Middleware
│       ├── Controllers/            # API Endpoints
│       ├── Middleware/             # Error Handling, Logging
│       ├── Extensions/             # DI Setup
│       └── Program.cs              # App Entry Point
├── tests/
│   ├── TicketSupport.UnitTests/    # Tests for Domain & Application
│   └── TicketSupport.IntegrationTests/ # Tests for API endpoints (with Docker/TestContainers)
├── docs/                           # Documentation
├── docker-compose.yml              # Container orchestration
└── README.md                       # Entry documentation
```

### Rationale (Why this architecture?)
- **Domain**: Is the heart of the business. No dependencies on DB or UI. Pure C#.
- **Application**: Orchestrates logic (Validation -> Domain -> Persistence).
- **Infrastructure**: Implements details (EF Core, SQL). Can be swapped without touching Domain logic.
- **API**: Thin layer (presentation). Only responsible for HTTP concerns (Status codes, headers).

## 2. Data Model (Deliverable C)

### Diagram (Textual)
- **Tenant** (1) ---- (N) **User** (Users belong to a Company)
- **User** (1) ---- (N) **Ticket** (Reporter/Assignee)
- **Tenant** (1) ---- (N) **Ticket** (Tickets belong to a specific Tenant)
- **Ticket** (1) ---- (N) **Comment** (Communication history)

### Entities

#### Entity: Tenant
| Field | Type | Required | Description |
|---|---|---|---|
| Id | Guid | Yes | PK |
| Name | String(100) | Yes | Company Name |
| PlanType | Enum | Yes | Basic, Premium |
| IsActive | Bool | Yes | Soft delete support |

#### Entity: User
| Field | Type | Required | Description |
|---|---|---|---|
| Id | Guid | Yes | PK |
| TenantId | Guid | Yes | FK to Tenant |
| FullName | String(100) | Yes | |
| Email | String(150) | Yes | Unique per Tenant or Global |
| Role | Enum | Yes | Admin, Agent, Customer |
| PasswordHash| String | Yes | (Simplified for demo) |

#### Entity: Ticket
| Field | Type | Required | Description |
|---|---|---|---|
| Id | Guid | Yes | PK |
| TenantId | Guid | Yes | FK (Multi-tenancy isolation) |
| Title | String(200) | Yes | |
| Description | String(Max) | Yes | |
| Status | Enum | Yes | Open, InProgress, Resolved, Closed |
| Priority | Enum | Yes | Low, Medium, High, Critical |
| CreatedByUserId| Guid | Yes | FK (Reporter) |
| AssignedToUserId| Guid | No | FK (Agent) |
| CreatedAt | DateTimeOps | Yes | |
| UpdatedAt | DateTimeOps | No | |

#### Entity: Comment
| Field | Type | Required | Description |
|---|---|---|---|
| Id | Guid | Yes | PK |
| TicketId | Guid | Yes | FK |
| UserId | Guid | Yes | Author |
| Content | String(1000)| Yes | |
| CreatedAt | DateTimeOps | Yes | |

## 3. Technology Choices
- **EF Core 8**: Standard ORM.
- **SQL Server / SQLite**: SQL Server for Prod, SQLite for simple local dev (swappable via connection string).
- **FluentValidation**: Separates validation rules from DTOs.
- **Serilog**: Structured logging (JSON) for observability.
- **xUnit**: Standard testing framework.
