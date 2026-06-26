using FinTrack.Domain.Enums;

namespace FinTrack.Application.Features.Transactions.CreateTransaction;

public class CreateTransactionRequest
{
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTimeOffset TransactionDate { get; set; }
    public string? Note { get; set; }
}