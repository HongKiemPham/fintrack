using FinTrack.Domain.Entities;
using FinTrack.Domain.Enums;

namespace FinTrack.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(FinTrackDbContext dbContext)
    {
        if (dbContext.Categories.Any())
            return;

        var categories = new List<Category>
        {
            new("Salary", TransactionType.Income),
            new("Bonus", TransactionType.Income),
            new("Investment", TransactionType.Income),

            new("Food", TransactionType.Expense),
            new("Transport", TransactionType.Expense),
            new("Rent", TransactionType.Expense),
            new("Utilities", TransactionType.Expense),
            new("Healthcare", TransactionType.Expense),
            new("Education", TransactionType.Expense),
            new("Entertainment", TransactionType.Expense)
        };

        await dbContext.Categories.AddRangeAsync(categories);
        await dbContext.SaveChangesAsync();
    }
}