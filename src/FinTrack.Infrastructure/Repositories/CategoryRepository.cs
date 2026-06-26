using FinTrack.Application.Common.Interfaces;
using FinTrack.Domain.Entities;
using FinTrack.Domain.Enums;
using FinTrack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly FinTrackDbContext _dbContext;

    public CategoryRepository(FinTrackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<bool> ExistsAsync(
        Guid id,
        TransactionType type,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.Categories
            .AnyAsync(x =>
                x.Id == id &&
                x.Type == type &&
                x.IsActive,
                cancellationToken);
    }
}