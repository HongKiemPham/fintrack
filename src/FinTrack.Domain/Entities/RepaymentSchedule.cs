using FinTrack.Domain.Common;
using FinTrack.Domain.Enums;

namespace FinTrack.Domain.Entities;

public class RepaymentSchedule : AuditableEntity
{
    private RepaymentSchedule() { }

    public RepaymentSchedule(
        Guid loanApplicationId,
        int periodNumber,
        DateTime dueDate,
        decimal principalAmount,
        decimal interestAmount)
    {
        if (loanApplicationId == Guid.Empty)
            throw new ArgumentException("LoanApplicationId is required.");

        if (periodNumber <= 0)
            throw new ArgumentException("Period number must be greater than zero.");

        if (principalAmount < 0)
            throw new ArgumentException("Principal amount cannot be negative.");

        if (interestAmount < 0)
            throw new ArgumentException("Interest amount cannot be negative.");

        LoanApplicationId = loanApplicationId;
        PeriodNumber = periodNumber;
        DueDate = dueDate;
        PrincipalAmount = principalAmount;
        InterestAmount = interestAmount;
        Status = RepaymentStatus.Pending;
    }

    public Guid LoanApplicationId { get; private set; }
    public int PeriodNumber { get; private set; }
    public DateTime DueDate { get; private set; }
    public decimal PrincipalAmount { get; private set; }
    public decimal InterestAmount { get; private set; }
    public decimal TotalAmount => PrincipalAmount + InterestAmount;
    public RepaymentStatus Status { get; private set; }
    public DateTime? PaidAt { get; private set; }

    public void MarkAsPaid()
    {
        if (Status == RepaymentStatus.Paid)
            throw new InvalidOperationException("Repayment is already paid.");

        Status = RepaymentStatus.Paid;
        PaidAt = DateTime.UtcNow;
        MarkAsUpdated();
    }
}