using FinTrack.Domain.Entities;
using FinTrack.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace FinTrack.Domain.Tests
{
    public class LoanApplicationTests
    {
        [Fact]
        public void Submit_Should_Change_Status_To_Submitted()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");

            // Act
            loan.Submit();

            // Assert
            loan.Status.Should().Be(LoanStatus.Submitted);
        }

        [Fact]
        public void Submit_Should_Throw_When_Loan_Is_Not_Draft()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");

            loan.Submit();

            // Act
            var act = () => loan.Submit();

            // Assert
            act.Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void StartReview_Should_Change_Status_To_UnderReview()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");

            loan.Submit();

            // Act
            loan.StartReview(Guid.NewGuid());


            // Assert
            loan.Status.Should().Be(LoanStatus.UnderReview);
        }

        [Fact]
        public void StartReview_Should_Throw_When_Loan_Is_Draft()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");

            // Act
            var act = () => loan.StartReview(Guid.NewGuid());

            // Assert
            act.Should()
                .Throw<InvalidOperationException>();
        }
    }
}
