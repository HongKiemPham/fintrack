# FinTrack - Business Rules

## 1. User & Authentication Rules

### BR-001: Unique Email
Mỗi email chỉ được đăng ký một tài khoản duy nhất.

### BR-002: Locked User
User bị khóa:

- Không thể đăng nhập.
- Không thể truy cập API được bảo vệ.
- JWT cũ phải bị từ chối nếu tài khoản đã bị khóa.

### BR-003: Role-Based Access
Hệ thống có 3 role chính:

- Admin
- Customer
- Finance Officer

Mỗi role chỉ được phép truy cập các chức năng được phân quyền.

## 2. Customer Data Rules

### BR-004: Customer Data Isolation
Customer chỉ được xem và thao tác dữ liệu thuộc về chính mình.

Customer không được xem:

- Transaction của user khác.
- Loan application của user khác.
- Repayment schedule của user khác.

## 3. Transaction Rules

### BR-005: Transaction Amount
Amount của transaction phải lớn hơn 0.

### BR-006: Transaction Type
Transaction chỉ có 2 loại:

- Income
- Expense

### BR-007: Transaction Category
Mỗi transaction phải thuộc về một category hợp lệ.

## 4. Loan Rules

### BR-008: Required Loan Fields
Một loan application bắt buộc có:

- Amount > 0
- InterestRate > 0
- LoanTermMonths > 0
- Purpose không được rỗng

### BR-009: Loan Status
Loan application có các trạng thái:

```text
Pending
Approved
Rejected
Closed
```

### BR-010: Loan Approval Permission
Chỉ Finance Officer được phép:

- Approve loan
- Reject loan

### BR-011: Loan Modification
Customer không được sửa loan application đã được:

- Approved
- Rejected
- Closed

## 5. Repayment Rules

### BR-012: Generate Repayment Schedule
Repayment schedule chỉ được sinh khi loan status là:

```text
Approved
```

### BR-013: Repayment Calculation
Mỗi kỳ trả nợ cần có:

- DueDate
- PrincipalAmount
- InterestAmount
- TotalAmount
- PaymentStatus

### BR-014: Mark Repayment Paid
Chỉ repayment chưa thanh toán mới được đánh dấu là paid.

## 6. Credit Score Integration Rules

### BR-015: Mock SOAP Credit Score
Khi customer tạo loan application hoặc khi Finance Officer review loan, hệ thống gọi mock SOAP/XML credit score service.

### BR-016: Store Credit Score Result
Kết quả credit score cần được lưu kèm loan application để phục vụ xét duyệt.

## 7. Reporting Rules

### BR-017: Monthly Report
Monthly report của Customer bao gồm:

- TotalIncome
- TotalExpense
- NetCashflow
- LoanBalance

### BR-018: Report Scope
Customer chỉ được xem report của chính mình.
