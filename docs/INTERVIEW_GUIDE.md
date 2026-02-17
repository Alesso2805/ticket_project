# Interview Guide (Deliverables F & G)

## The "Pitch" (2 Minutes)
*Goal: Explain the project clearly, showing problem-solving skills and technical depth.*

"I built a B2B Ticket Support System to solve the problem of managing support requests across multiple client companies (Tenants). 

**The Challenge**: 
The main challenge was designing a scalable, maintainable architecture that could handle multi-tenancy basics and complex data relationships, while ensuring high performance and testability.

**The Solution**:
I implemented a REST API using **.NET 8** and **Clean Architecture** to decouple business rules from infrastructure. 
- For data, I used **EF Core** with **SQL Server**, implementing relationships like One-to-Many for Tenants and Tickets.
- To ensure responsiveness, I used **Async/Await** all the way down to avoid thread blocking.
- Reliability is handled via a **Global Exception Handler** returning standard `ProblemDetails`.
- Quality is ensured with **xUnit** tests and Integration tests using **TestContainers**.

**Result**:
A production-ready code structure that is containerized with Docker, easy to extend, and follows SOLID principles."

---

## Technical Q&A (Interview Prep)

### 1. Why Async/Await?
**Q:** Why did you use `Task<IActionResult>` instead of just `IActionResult`?
**A:** Because database operations (I/O) are slow. Synchronous code blocks the thread while waiting. `Async/await` releases the thread back to the thread pool to handle other requests while waiting for the DB, drastically improving **throughput** and Scalability under load.

### 2. Dependency Injection (DI)
**Q:** How does DI work in .NET?
**A:** .NET has a built-in container. We register services (interfaces and implementations) in `Program.cs` with lifetimes:
- **Transient**: New instance every time.
- **Scoped**: One instance per HTTP Request (Best for DbContext).
- **Singleton**: One instance for the app lifetime.

### 3. Entity Framework Core Performance
**Q:** How do you avoid N+1 problems?
**A:** By using `.Include(t => t.Comments)` to eagerly load related data when needed, or by using Projection (`.Select(t => new Dto { ... })`) to fetch only necessary fields, which is the most performant way.

### 4. Clean Architecture
**Q:** Why separate Domain and Application?
**A:** To keep the business logic pure. `Domain` entities shouldn't know about DTOs or Database libraries. `Application` handles the "What to do" (Use Cases), while `Infrastructure` handles "How to store it". This makes it easy to switch databases or add mock testing.

### 5. Indexing (SQL)
**Q:** What indexes would you add?
**A:** 
- `IX_Tickets_TenantId` (Since we always filter by Tenant).
- `IX_Tickets_Status` (For status filtering).
- `IX_Tickets_CreatedBy` (To find user tickets quickly).
Indexes speed up reads but slow down writes slightly; it's a trade-off.
