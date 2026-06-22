# FinTrack - Sprint 1 Planning

## Sprint Duration

2 weeks

## Sprint Goal

Build the backend foundation for FinTrack with Clean Architecture, ASP.NET Core Web API, SQL Server, authentication, authorization, validation, logging, exception handling, and Swagger.

The goal is not to complete all business features yet. The goal is to create a strong production-ready foundation that future modules can build on.

---

## Sprint 1 Scope

### Task 1: Create Solution Structure

Create .NET solution with Clean Architecture projects:

```text
FinTrack.sln
src/
├── FinTrack.Api
├── FinTrack.Application
├── FinTrack.Domain
└── FinTrack.Infrastructure

tests/
├── FinTrack.Application.Tests
└── FinTrack.Api.Tests
```

Expected result:

- Solution builds successfully.
- Project references follow Clean Architecture dependency rule.

---

### Task 2: Configure ASP.NET Core Web API

Setup:

- Controllers
- Swagger
- appsettings.json
- Environment-specific configuration

Expected result:

- API runs successfully.
- Swagger opens at `/swagger`.

---

### Task 3: Configure SQL Server and EF Core

Setup:

- SQL Server connection string
- ApplicationDbContext
- EF Core packages
- First migration

Expected result:

- Migration can be created.
- Database can be updated.

---

### Task 4: Configure Identity and Authentication

Setup:

- ASP.NET Core Identity
- JWT authentication
- Role-based authorization
- Default roles: Admin, Customer, FinanceOfficer

Expected result:

- Register API works.
- Login API returns JWT token.
- Protected API returns 401 without token.

---

### Task 5: Create Auth APIs

Required endpoints:

```http
POST /api/auth/register
POST /api/auth/login
GET /api/auth/me
```

Expected result:

- Customer can register.
- User can login.
- Authenticated user can get profile.

---

### Task 6: Add Validation

Setup:

- FluentValidation
- Request DTO validation

Expected result:

- Invalid request returns 400.
- Validation messages are clear.

---

### Task 7: Add Global Exception Handling

Setup:

- Custom exception middleware
- Standard error response format

Expected result:

- Unhandled exception does not expose stack trace.
- API returns consistent error response.

---

### Task 8: Add Logging

Setup:

- Serilog
- Console logging
- File logging if needed

Expected result:

- API request and error logs are visible.

---

## Definition of Done

Sprint 1 is done when:

- Solution uses Clean Architecture structure.
- API runs with `dotnet run`.
- Swagger works.
- SQL Server connection works.
- EF Core migration works.
- Register API works.
- Login API works.
- JWT authentication works.
- Role-based authorization is configured.
- FluentValidation is configured.
- Global exception handling is configured.
- Serilog logging is configured.
- Code is committed to Git.

---

## Suggested Git Commits

```bash
git add .
git commit -m "chore: create clean architecture solution structure"
```

```bash
git add .
git commit -m "feat: configure database and identity"
```

```bash
git add .
git commit -m "feat: add authentication APIs"
```

```bash
git add .
git commit -m "chore: add validation logging and exception handling"
```
