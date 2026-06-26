using FinTrack.Infrastructure;
using FinTrack.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using FinTrack.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "FinTrack API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.MapGet("/health", () => Results.Ok(new
{
    status = "Healthy",
    application = "FinTrack.Api"
}));


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FinTrackDbContext>();

    await dbContext.Database.MigrateAsync();

    await DbSeeder.SeedAsync(dbContext);
}

app.Run();