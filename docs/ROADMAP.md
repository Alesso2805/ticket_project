# Mentoring Roadmap (Deliverable D)

A step-by-step guide to building this project in ~10-14 days (1-2 hours/day).

## Phase 1: Foundation (Days 1-2)
- [ ] **Setup Solution**: Create Clean Architecture structure.
- [ ] **Domain Layer**: Implement Entities (`Ticket`, `User`, `Tenant`) and Enums.
- [ ] **Infrastructure Layer**: Setup `AppDbContext`, `appsettings.json`, and initial Migration.
- [ ] **Verify**: Ensure the DB is created (using `dotnet ef database update`).

## Phase 2: Core Logic & CRUD (Days 3-5)
- [ ] **Application Layer**: Create `DTOs` and `ITicketService`.
- [ ] **API Layer**: Create `TicketsController` with basic CRUD (Get, Post).
- [ ] **Dependency Injection**: Register implementations in `Program.cs`.
- [ ] **Validation**: Add `FluentValidation` to incoming DTOs.
- [ ] **Exception Handling**: Implement global Middleware for error responses.

## Phase 3: Advanced Features & Auth (Days 6-8)
- [ ] **Filtering/Pagination**: Implement logic in Service/Repository to handle `page` and `pageSize`.
- [ ] **Authentication**: Implement JWT generation.
- [ ] **Authorization**: Add `[Authorize(Roles = "Admin")]` to specific endpoints.
- [ ] **Business Rules**: Ensure only Admins/Agents can assign tickets.

## Phase 4: Testing & Quality (Days 9-11)
- [ ] **Unit Tests**: Test `TicketService` logic (mocking `MainDbContext` or Repository).
- [ ] **Integration Tests**: Set up `TestContainers` (or SQLite InMem) to test the Controller -> DB flow.
- [ ] **Logging**: Configure Serilog to log to Console/File.

## Phase 5: DevOps & Documentation (Days 12-14)
- [ ] **Docker**: Create `Dockerfile` for API and `docker-compose` for SQL Server.
- [ ] **Documentation**: Complete README and Swagger examples.
- [ ] **Review**: Practice the "Pitch" (see `INTERVIEW_GUIDE.md`).

## Daily Goal
Focus on one layer/component at a time. Do not try to write the entire API at once. Get one endpoint working end-to-end first (Vertical Slice approach).
