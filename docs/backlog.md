# FinTrack - Product Backlog

## Priority Definition

- P0: Must Have for MVP
- P1: Should Have after MVP core works
- P2: Could Have for future enhancement

---

## P0 - Must Have

### Authentication & Authorization

- Register Customer account
- Login with JWT
- Get current user profile
- Role-based authorization
- Password hashing
- Lock user handling

### User Management

- List users
- Lock user
- Unlock user
- Assign role

### Transaction Management

- Create income transaction
- Create expense transaction
- Update transaction
- Delete transaction
- View transaction history
- Search and filter transactions
- Pagination
- Category support

### Loan Management

- Create loan application
- Get my loan applications
- View pending loan applications
- Approve loan
- Reject loan
- Store approval or rejection note
- Store credit score result

### Repayment Schedule

- Generate repayment schedule after loan approval
- View repayment schedule
- Mark repayment as paid

### Reporting

- Monthly cashflow report
- Loan summary report

### SOAP/XML Integration

- Mock credit score SOAP/XML request
- Mock credit score SOAP/XML response
- Adapter service in Infrastructure layer

### Infrastructure & Production Readiness

- Clean Architecture solution structure
- SQL Server integration
- EF Core migration
- FluentValidation
- Global exception handling
- Serilog logging
- Swagger/OpenAPI
- Basic unit tests for business logic

---

## P1 - Should Have

### Frontend Dashboard

- React login page
- React transaction management page
- React loan application page
- React officer approval page
- Dashboard charts

### Export & Reporting

- Export transactions to CSV
- Export monthly report to CSV

### Notification

- Email notification when loan is approved
- Email notification when loan is rejected

### Testing

- Integration tests for API endpoints
- Repository tests with test database

---

## P2 - Could Have

### Advanced Finance Features

- Recurring transactions
- Multi-currency support
- Budget planning
- Spending limit alerts

### Advanced Architecture

- Background jobs
- Redis cache
- Docker Compose
- CI/CD pipeline with GitHub Actions

### Future Product Features

- Mobile app
- Real bank integration
- Real payment gateway
- AI financial recommendation
