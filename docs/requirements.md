# FinTrack – Personal Finance & Loan Management Platform

## 1. Project Overview

FinTrack is a fullstack personal finance and loan management platform built with ASP.NET Core Web API, ReactJS, and SQL Server.

The project is designed as a portfolio-ready application for demonstrating practical .NET backend development, Clean Architecture, RESTful API design, SQL Server database design, authentication, authorization, logging, validation, exception handling, reporting, and legacy SOAP/XML integration.

## 2. Goals

- Build a real-world fullstack finance application.
- Apply Clean Architecture in ASP.NET Core.
- Design RESTful APIs.
- Use SQL Server with proper schema design and indexing.
- Implement JWT authentication and role-based authorization.
- Implement validation, logging, global exception handling.
- Provide reporting features.
- Simulate integration with a legacy SOAP/XML credit score service.
- Organize development using Agile backlog, sprint, user stories, and acceptance criteria.

## 3. MVP Scope

The MVP includes:

- Authentication and authorization
- User management
- Personal income and expense tracking
- Loan application management
- Loan approval workflow
- Repayment schedule generation
- Monthly financial reporting
- Mock SOAP/XML credit score integration

## 4. Out of Scope for MVP

The following features are not included in the MVP:

- Real bank integration
- Real payment gateway
- Mobile application
- AI-based financial recommendations
- Multi-currency support
- Real-time notifications
- Microservices architecture

## 5. Actors

### Admin

Admin can:

- View users
- Lock and unlock users
- Assign roles
- View system-level overview

### Customer

Customer can:

- Register and login
- Manage personal income and expenses
- Create loan applications
- View loan status
- View repayment schedules
- View personal financial reports

### Finance Officer

Finance Officer can:

- View pending loan applications
- Review customer financial data
- View mock credit score result
- Approve or reject loan applications
- Add approval or rejection notes

## 6. Core Modules

### 6.1 Authentication & Authorization

Features:

- Register
- Login
- JWT authentication
- Role-based authorization

### 6.2 User Management

Features:

- View users
- Lock or unlock users
- Assign roles

### 6.3 Transaction Management

Features:

- Create income transaction
- Create expense transaction
- Update transaction
- Delete transaction
- Filter transactions by date, type, and category
- View transaction summary

### 6.4 Loan Management

Features:

- Customer creates loan application
- Finance Officer reviews application
- Finance Officer approves or rejects application
- System stores loan status and review notes

### 6.5 Repayment Schedule

Features:

- Generate repayment schedule after loan approval
- Calculate principal, interest, and total payment
- Mark repayment as paid

### 6.6 Reporting

Features:

- Monthly income summary
- Monthly expense summary
- Net cashflow
- Loan summary

### 6.7 SOAP/XML Credit Score Integration

Features:

- Mock credit score service
- XML request and response format
- Used during loan application review

## 7. Initial Business Rules

- A customer can only view their own transactions and loans.
- A finance officer can view pending loan applications.
- Only a finance officer can approve or reject loans.
- Only an admin can manage users and roles.
- A loan must have amount, term, interest rate, and purpose.
- A repayment schedule is generated only after a loan is approved.
- Credit score is fetched from a mock SOAP/XML service when a loan application is created or reviewed.

## 8. Initial Non-Functional Requirements

- API must use RESTful conventions.
- Backend must follow Clean Architecture.
- Validation must be implemented for all input DTOs.
- Global exception handling must be implemented.
- Logging must be implemented.
- Sensitive endpoints must require authentication.
- Authorization must be role-based.
- Database must use indexes for frequently queried columns.
- Project must include README documentation.
- Project should include unit tests for business logic.

## 9. Success Criteria for MVP

The MVP is considered complete when:

- A customer can register and login.
- A customer can create and manage income/expense transactions.
- A customer can apply for a loan.
- A finance officer can approve or reject the loan.
- A repayment schedule is generated after approval.
- A customer can view reports.
- Admin can manage users.
- SOAP/XML mock credit score integration works.
- Project has documentation and can be presented on GitHub.