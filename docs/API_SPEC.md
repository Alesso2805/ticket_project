# API Specification (Deliverable B)

Base URL: `/api/v1`

## Authentication
- **POST** `/auth/login`
  - Body: `{ email, password }`
  - Response: `{ token, expiration }` (JWT)

## Tickets
- **GET** `/tickets`
  - Query Params: 
    - `page` (int, default 1)
    - `pageSize` (int, default 10)
    - `status` (enum, optional)
    - `proirity` (enum, optional)
    - `assignedTo` (guid, optional)
  - Headers: `Authorization: Bearer <token>`
  - Response: `PagedResult<TicketDto>`

- **GET** `/tickets/{id}`
  - Response: `TicketDetailDto` (includes comments summary)

- **POST** `/tickets`
  - Body: `{ title, description, priority }`
  - Response: `201 Created` + Location Header

- **PUT** `/tickets/{id}`
  - Body: `{ title, description, priority, status }`
  - Note: Full update.

- **PATCH** `/tickets/{id}/status`
  - Body: `{ status }`
  - Note: Specific status transition logic implementation.

- **PATCH** `/tickets/{id}/assign`
  - Body: `{ assignedToUserId }`
  - Role: Agent/Admin only.

- **POST** `/tickets/{id}/comments`
  - Body: `{ content }`
  - Response: `201 Created`

## Users (Admin Only)
- **POST** `/users` (Create Agent/Customer)
- **GET** `/users`

## Common Response Formats

### Error (ProblemDetails)
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Validation Error",
  "status": 400,
  "errors": {
    "Title": ["The Title field is required."]
  }
}
```
