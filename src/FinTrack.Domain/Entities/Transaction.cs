using FinTrack.Domain.Common;
using FinTrack.Domain.Enums;

namespace FinTrack.Domain.Entities;

public class Transaction : AuditableEntity
{
    private Transaction() { }

    public Transaction(
        Guid userId,
        Guid categoryId,
        decimal amount,
        TransactionType type,
        DateTime transactionDate,
        string? note)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId is required.");

        if (categoryId == Guid.Empty)
            throw new ArgumentException("CategoryId is required.");

        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        UserId = userId;
        CategoryId = categoryId;
        Amount = amount;
        Type = type;
        TransactionDate = transactionDate;
        Note = note;
    }

    public Guid UserId { get; private set; }
    public Guid CategoryId { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionType Type { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public string? Note { get; private set; }

    public void Update(decimal amount, DateTime transactionDate, string? note)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        Amount = amount;
        TransactionDate = transactionDate;
        Note = note;
        MarkAsUpdated();
    }
}