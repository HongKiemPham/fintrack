using FinTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinTrack.Infrastructure.Persistence.Configurations;

public class LoanApplicationConfiguration : IEntityTypeConfiguration<LoanApplication>
{
    public void Configure(EntityTypeBuilder<LoanApplication> builder)
    {
        builder.ToTable("LoanApplications");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.TermMonths)
            .IsRequired();

        builder.Property(x => x.InterestRate)
            .HasPrecision(9, 4)
            .IsRequired();

        builder.Property(x => x.Purpose)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.CreditScore);

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.ReviewNote)
            .HasMaxLength(1000);

        builder.Property(x => x.ReviewedByUserId);

        builder.Property(x => x.ApprovedAt);

        builder.Property(x => x.RejectedAt);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.Status);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}