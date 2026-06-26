using FinTrack.Application.Common.Interfaces;
using FinTrack.Infrastructure.Persistence;

namespace FinTrack.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly FinTrackDbContext _dbContext;

    public UnitOfWork(FinTrackDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}