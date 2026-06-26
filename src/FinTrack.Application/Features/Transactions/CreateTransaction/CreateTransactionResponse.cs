namespace FinTrack.Application.Features.Transactions.CreateTransaction;

public class CreateTransactionResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTimeOffset TransactionDate { get; set; }
    public string? Note { get; set; }
}