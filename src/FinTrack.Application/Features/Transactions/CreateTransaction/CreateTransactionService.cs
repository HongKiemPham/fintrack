using FinTrack.Application.Common.Interfaces;
using FinTrack.Domain.Entities;

namespace FinTrack.Application.Features.Transactions.CreateTransaction;

public class CreateTransactionService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransactionService(
        ICategoryRepository categoryRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateTransactionResponse> CreateAsync(
        CreateTransactionRequest request,
        CancellationToken cancellationToken = default)
    {
        var categoryExists = await _categoryRepository.ExistsAsync(
            request.CategoryId,
            request.Type,
            cancellationToken);

        if (!categoryExists)
            throw new InvalidOperationException("Category does not exist or does not match transaction type.");

        var transaction = new Transaction(
            request.UserId,
            request.CategoryId,
            request.Amount,
            request.Type,
            request.TransactionDate,
            request.Note);

        await _transactionRepository.AddAsync(transaction, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateTransactionResponse
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            TransactionDate = transaction.TransactionDate,
            Note = transaction.Note
        };
    }
}