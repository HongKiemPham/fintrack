# FinTrack - User Stories

## Epic 1: Authentication & Authorization

### US-001: Register Customer Account
As a Customer,  
I want to register an account,  
So that I can use the FinTrack system.

#### Acceptance Criteria

- Given email chưa tồn tại
- When Customer submit registration form
- Then account được tạo thành công
- And role mặc định là Customer
- And password được hash trước khi lưu database

---

### US-002: Login
As a User,  
I want to login,  
So that I can access protected features.

#### Acceptance Criteria

- Given email và password hợp lệ
- When User login
- Then hệ thống trả về JWT token
- And token chứa user id, email và role
- And locked user không thể login

---

### US-003: Get Current User Profile
As a logged-in User,  
I want to view my profile,  
So that I know which account I am using.

#### Acceptance Criteria

- Given User có JWT hợp lệ
- When gọi API `/api/auth/me`
- Then hệ thống trả về thông tin user hiện tại
- And không trả về password hash

---

## Epic 2: Transaction Management

### US-004: Create Income Transaction
As a Customer,  
I want to create an income transaction,  
So that I can track my money inflow.

#### Acceptance Criteria

- Given Customer đã login
- And amount > 0
- And category hợp lệ
- When tạo income transaction
- Then transaction được lưu thành công
- And transaction thuộc về Customer hiện tại

---

### US-005: Create Expense Transaction
As a Customer,  
I want to create an expense transaction,  
So that I can track my spending.

#### Acceptance Criteria

- Given Customer đã login
- And amount > 0
- And category hợp lệ
- When tạo expense transaction
- Then transaction được lưu thành công
- And transaction thuộc về Customer hiện tại

---

### US-006: View Transaction History
As a Customer,  
I want to view my transaction history,  
So that I can review my personal finances.

#### Acceptance Criteria

- Given Customer đã login
- When xem danh sách transactions
- Then chỉ trả về transaction của Customer hiện tại
- And có phân trang
- And có filter theo date range, type và category

---

### US-007: Update Transaction
As a Customer,  
I want to update my transaction,  
So that I can correct wrong information.

#### Acceptance Criteria

- Given transaction thuộc về Customer hiện tại
- When Customer cập nhật transaction
- Then transaction được cập nhật thành công
- And Customer không thể cập nhật transaction của user khác

---

### US-008: Delete Transaction
As a Customer,  
I want to delete my transaction,  
So that I can remove invalid records.

#### Acceptance Criteria

- Given transaction thuộc về Customer hiện tại
- When Customer xóa transaction
- Then transaction được xóa hoặc soft delete
- And Customer không thể xóa transaction của user khác

---

## Epic 3: Loan Management

### US-009: Apply For Loan
As a Customer,  
I want to apply for a loan,  
So that I can request borrowing money.

#### Acceptance Criteria

- Given Customer đã login
- And amount > 0
- And loan term > 0
- And purpose không rỗng
- When Customer submit loan application
- Then loan application được tạo với status Pending
- And credit score được lấy từ mock SOAP/XML service
- And credit score result được lưu

---

### US-010: View My Loan Applications
As a Customer,  
I want to view my loan applications,  
So that I can track approval status.

#### Acceptance Criteria

- Given Customer đã login
- When Customer xem danh sách loan applications
- Then chỉ trả về loan applications của Customer hiện tại

---

### US-011: View Pending Loans
As a Finance Officer,  
I want to view pending loan applications,  
So that I can review them.

#### Acceptance Criteria

- Given User có role Finance Officer
- When gọi pending loan API
- Then hệ thống trả về danh sách loan có status Pending
- And Customer không được truy cập API này

---

### US-012: Approve Loan
As a Finance Officer,  
I want to approve a loan,  
So that the customer receives a repayment schedule.

#### Acceptance Criteria

- Given loan status là Pending
- When Finance Officer approve loan
- Then loan status chuyển thành Approved
- And approval note được lưu nếu có
- And repayment schedule được sinh tự động

---

### US-013: Reject Loan
As a Finance Officer,  
I want to reject a loan,  
So that invalid applications are denied.

#### Acceptance Criteria

- Given loan status là Pending
- When Finance Officer reject loan
- Then loan status chuyển thành Rejected
- And rejection reason bắt buộc phải có
- And repayment schedule không được sinh

---

## Epic 4: Repayment Schedule

### US-014: View Repayment Schedule
As a Customer,  
I want to view my repayment schedule,  
So that I know when and how much to pay.

#### Acceptance Criteria

- Given loan đã Approved
- When Customer xem repayment schedule
- Then hệ thống trả về danh sách kỳ trả nợ
- And Customer chỉ xem được repayment schedule của loan thuộc về mình

---

### US-015: Mark Repayment As Paid
As a Finance Officer,  
I want to mark a repayment as paid,  
So that the loan payment status is updated.

#### Acceptance Criteria

- Given repayment chưa paid
- When Finance Officer mark as paid
- Then repayment status chuyển thành Paid
- And paid date được lưu

---

## Epic 5: Reporting

### US-016: View Monthly Cashflow Report
As a Customer,  
I want to view monthly cashflow report,  
So that I understand my financial health.

#### Acceptance Criteria

- Given Customer đã login
- When chọn tháng cần xem report
- Then hệ thống trả về TotalIncome, TotalExpense và NetCashflow
- And dữ liệu chỉ thuộc về Customer hiện tại

---

### US-017: View Loan Summary Report
As a Customer,  
I want to view loan summary report,  
So that I understand my debt situation.

#### Acceptance Criteria

- Given Customer đã login
- When xem loan summary
- Then hệ thống trả về total loan amount, paid amount và remaining balance

---

## Epic 6: Administration

### US-018: View Users
As an Admin,  
I want to view users,  
So that I can manage system accounts.

#### Acceptance Criteria

- Given User có role Admin
- When gọi list users API
- Then hệ thống trả về danh sách users
- And không trả về password hash

---

### US-019: Lock Or Unlock User
As an Admin,  
I want to lock or unlock a user,  
So that I can control account access.

#### Acceptance Criteria

- Given User có role Admin
- When lock user
- Then user không thể login
- When unlock user
- Then user có thể login lại nếu credential hợp lệ

---

### US-020: Assign Role
As an Admin,  
I want to assign roles,  
So that users have correct permissions.

#### Acceptance Criteria

- Given User có role Admin
- When assign role cho user
- Then role mới được cập nhật
- And chỉ role hợp lệ được chấp nhận
