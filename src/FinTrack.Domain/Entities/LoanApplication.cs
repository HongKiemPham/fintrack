using FinTrack.Domain.Common;
using FinTrack.Domain.Enums;

namespace FinTrack.Domain.Entities;

public class LoanApplication : AuditableEntity
{
    private LoanApplication() { }

    public LoanApplication(
        Guid userId,
        decimal amount,
        int termMonths,
        decimal interestRate,
        string purpose,
        int? creditScore = null)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId is required.");

        if (amount <= 0)
            throw new ArgumentException("Loan amount must be greater than zero.");

        if (termMonths <= 0)
            throw new ArgumentException("Loan term must be greater than zero.");

        if (interestRate <= 0)
            throw new ArgumentException("Interest rate must be greater than zero.");

        if (string.IsNullOrWhiteSpace(purpose))
            throw new ArgumentException("Loan purpose is required.");

        UserId = userId;
        Amount = amount;
        TermMonths = termMonths;
        InterestRate = interestRate;
        Purpose = purpose;
        CreditScore = creditScore;
        Status = LoanStatus.Draft;
    }

    public Guid UserId { get; private set; }
    public decimal Amount { get; private set; }
    private decimal _totalRepaid;
    public decimal TotalRepaid => _totalRepaid;
    public int TermMonths { get; private set; }
    public decimal InterestRate { get; private set; }
    public string Purpose { get; private set; } = default!;
    public int? CreditScore { get; private set; }
    public LoanStatus Status { get; private set; }
    public string? ReviewNote { get; private set; }
    public DateTimeOffset? ApprovedAt { get; private set; }
    public DateTimeOffset? RejectedAt { get; private set; }
    public Guid? ReviewedByUserId { get; private set; }
    public Guid? ApprovalId { get; private set; }

    public void Submit()
    {
        if (Status != LoanStatus.Draft)
            throw new InvalidOperationException(
                "Only draft loan can be submitted.");

        Status = LoanStatus.Submitted;

        MarkAsUpdated();
    }
    public void StartReview(Guid loanOfficerId)
    {
        if (Status != LoanStatus.Submitted)
            throw new InvalidOperationException(
                "Only submitted loan can start review.");

        Status = LoanStatus.UnderReview;
        ReviewedByUserId = loanOfficerId;

        MarkAsUpdated();
    }
    public void Approve(Guid approverId, string? reviewNote)
    {
        if (Status != LoanStatus.UnderReview)
            throw new InvalidOperationException(
                "Only loan under review can be approved.");

        Status = LoanStatus.Approved;
        ReviewNote = reviewNote;
        ReviewedByUserId = approverId;
        ApprovedAt = DateTimeOffset.UtcNow;

        MarkAsUpdated();
    }
    public void Disburse()
    {
        if (Status != LoanStatus.Approved)
            throw new InvalidOperationException(
                "Only approved loan can be disbursed.");

        Status = LoanStatus.Disbursed;

        MarkAsUpdated();
    }
    public void RecordRepayment(decimal amount)
    {
        if (Status != LoanStatus.Disbursed &&
            Status != LoanStatus.Repaying)
            throw new InvalidOperationException(
                "Only disbursed or repaying loan can receive repayment.");

        if (amount <= 0)
            throw new ArgumentException(
                "Repayment amount must be greater than zero.",
                nameof(amount));

        if (_totalRepaid + amount > Amount)
            throw new InvalidOperationException(
                "Total repayment cannot exceed loan amount.");

        _totalRepaid += amount;

        Status = _totalRepaid == Amount
        ? LoanStatus.Completed
        : LoanStatus.Repaying;

        MarkAsUpdated();
    }

    public void Reject(Guid rejectorId, string? reviewNote)
    {
        if (Status != LoanStatus.UnderReview)
            throw new InvalidOperationException(
                "Only pending loan can be rejected.");

        if (string.IsNullOrWhiteSpace(reviewNote))
            throw new ArgumentException(
                "Reject reason is required.");

        Status = LoanStatus.Rejected;
        ReviewNote = reviewNote;
        ReviewedByUserId = rejectorId;
        RejectedAt = DateTimeOffset.UtcNow;

        MarkAsUpdated();
    }
    public void MarkApprovalCompleted()
    {
        if (Status != LoanStatus.UnderReview)
            throw new InvalidOperationException(
                "Only loan under review can have completed approval.");

        Status = LoanStatus.Approved;

        MarkAsUpdated();
    }
    public void AttachApproval(Guid approvalId)
    {
        if (approvalId == Guid.Empty)
            throw new ArgumentException(
                "ApprovalId is required.",
                nameof(approvalId));

        if (ApprovalId.HasValue)
            throw new InvalidOperationException(
                "An approval is already attached to this loan.");

        ApprovalId = approvalId;
        MarkAsUpdated();
    }




}