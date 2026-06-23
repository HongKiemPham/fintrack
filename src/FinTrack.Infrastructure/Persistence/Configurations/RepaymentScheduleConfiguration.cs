using FinTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinTrack.Infrastructure.Persistence.Configurations;

public class RepaymentScheduleConfiguration : IEntityTypeConfiguration<RepaymentSchedule>
{
    public void Configure(EntityTypeBuilder<RepaymentSchedule> builder)
    {
        builder.ToTable("RepaymentSchedules");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PeriodNumber)
            .IsRequired();

        builder.Property(x => x.DueDate)
            .IsRequired();

        builder.Property(x => x.PrincipalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.InterestAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.PaidAt);

        builder.HasIndex(x => x.LoanApplicationId);

        builder.HasIndex(x => new { x.LoanApplicationId, x.PeriodNumber })
            .IsUnique();

        builder.HasOne<LoanApplication>()
            .WithMany()
            .HasForeignKey(x => x.LoanApplicationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}