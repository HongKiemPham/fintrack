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
        Status = LoanStatus.Pending;
    }

    public Guid UserId { get; private set; }
    public decimal Amount { get; private set; }
    public int TermMonths { get; private set; }
    public decimal InterestRate { get; private set; }
    public string Purpose { get; private set; } = default!;
    public int? CreditScore { get; private set; }
    public LoanStatus Status { get; private set; }
    public string? ReviewNote { get; private set; }

    public void Approve(string? reviewNote)
    {
        if (Status != LoanStatus.Pending)
            throw new InvalidOperationException("Only pending loan can be approved.");

        Status = LoanStatus.Approved;
        ReviewNote = reviewNote;
        MarkAsUpdated();
    }

    public void Reject(string reviewNote)
    {
        if (Status != LoanStatus.Pending)
            throw new InvalidOperationException("Only pending loan can be rejected.");

        if (string.IsNullOrWhiteSpace(reviewNote))
            throw new ArgumentException("Reject reason is required.");

        Status = LoanStatus.Rejected;
        ReviewNote = reviewNote;
        MarkAsUpdated();
    }
}