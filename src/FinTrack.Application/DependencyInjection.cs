using FinTrack.Application.Features.Transactions.CreateTransaction;
using Microsoft.Extensions.DependencyInjection;

namespace FinTrack.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateTransactionService>();

        return services;
    }
}