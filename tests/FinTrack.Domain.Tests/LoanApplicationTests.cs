using FinTrack.Domain.Entities;
using FinTrack.Domain.Enums;
using FluentAssertions;
using System.Reflection.Emit;
using Xunit;

namespace FinTrack.Domain.Tests
{
    public class LoanApplicationTests
    {
        #region Submit Loan
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

        #endregion

        #region StartReview Loan
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
        #endregion

        #region Approve Loan
        [Fact]
        public void Approve_Should_Change_Status_To_Approved()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            // Act
            loan.Approve(Guid.NewGuid(), "Approved");
            // Assert
            loan.Status.Should().Be(LoanStatus.Approved);
        }
        [Fact]
        public void Approve_Should_Throw_When_Loan_Is_Draft()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            // Act
            var act = () => loan.Approve(Guid.NewGuid(), "Approved");
            // Assert
            act.Should()
                .Throw<InvalidOperationException>();
        }
        [Fact]
        public void Approve_Should_Throw_When_Loan_Is_Approved()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            loan.Approve(Guid.NewGuid(), "Approved");

            // Act
            var act = () => loan.Approve(Guid.NewGuid(), null);
            // Assert
            act.Should()
                .Throw<InvalidOperationException>();
        }
        [Fact]
        public void MarkApprovalCompleted_Should_Change_Status_To_Approved()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");

            loan.Submit();
            loan.StartReview(Guid.NewGuid());

            // Act
            loan.MarkApprovalCompleted();

            // Assert
            loan.Status.Should().Be(LoanStatus.Approved);
        }
        [Fact]
        public void MarkApprovalCompleted_Should_Throw_When_Loan_Is_Not_UnderReview()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");

            // Act
            var act = () => loan.MarkApprovalCompleted();

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }

        #endregion

        #region Reject Loan

        [Fact]
        public void Reject_Should_Change_Status_To_Rejected()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            // Act
            loan.Reject(Guid.NewGuid(), "Rejected");
            // Assert
            loan.Status.Should().Be(LoanStatus.Rejected);
        }

        [Fact]
        public void Reject_Should_Throw_When_Loan_Is_Draft()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            // Act
            var act = () => loan.Reject(Guid.NewGuid(), "Rejected");
            // Assert
            act.Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void Reject_Should_Throw_When_Loan_Is_Approved()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            loan.Approve(Guid.NewGuid(), "Approved");
            // Act
            var act = () => loan.Reject(Guid.NewGuid(), "Rejected");
            // Assert
            act.Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void Reject_Should_Throw_When_RejectReason_Is_NullOrEmpty()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            // Act
            var act = () => loan.Reject(Guid.NewGuid(), null);
            // Assert
            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void Reject_Should_Throw_When_RejectReason_Is_Whitespace()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            // Act
            var act = () => loan.Reject(Guid.NewGuid(), "   ");
            // Assert
            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void Reject_Should_Throw_When_RejectReason_Is_Empty()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            // Act
            var act = () => loan.Reject(Guid.NewGuid(), "");
            // Assert
            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void Reject_Should_Throw_When_Loan_Is_Rejected()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            loan.Reject(Guid.NewGuid(), "Rejected");
            // Act
            var act = () => loan.Reject(Guid.NewGuid(), "Rejected");
            // Assert
            act.Should()
                .Throw<InvalidOperationException>();
        }
        #endregion

        #region Desbursed
        [Fact]
        public void Disburse_Should_Change_Status_To_Disbursed()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            loan.Approve(Guid.NewGuid(), "Approved");
            // Act
            loan.Disburse();
            // Assert
            loan.Status.Should().Be(LoanStatus.Disbursed);
        }

        [Fact]
        public void Disburse_Should_Throw_When_Loan_Is_Draft()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            // Act
            var act = () => loan.Disburse();
            // Assert
            act.Should()
                .Throw<InvalidOperationException>();
        }
        [Fact]
        public void Disburse_Should_Throw_When_Loan_Is_UnderReview()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            // Act
            var act = () => loan.Disburse();
            // Assert
            act.Should()
                .Throw<InvalidOperationException>();
        }
        [Fact]
        public void Disburse_Should_Throw_When_Loan_Is_Submitted()
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
            var act = () => loan.Disburse();
            // Assert
            act.Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public void Disburse_Should_Throw_When_Loan_Is_Rejected()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            loan.Reject(Guid.NewGuid(), "Insufficient credit score");
            // Act
            var act = () => loan.Disburse();
            // Assert
            act.Should()
                .Throw<InvalidOperationException>();
        }
        #endregion

        #region Repayment
        [Fact]
        public void RecordRepayment_WhenLoanIsDisbursed_ShouldChangeStatusToRepaying()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            loan.Approve(Guid.NewGuid(), "Approved");
            loan.Disburse();
            // Act
            loan.RecordRepayment(20000000);
            // Assert
            loan.Status.Should().Be(LoanStatus.Repaying);
            loan.TotalRepaid.Should().Be(20_000_000);
        }
        [Fact]
        public void RecordRepayment_WhenLoanIsNotDisbursed_ShouldThrowException()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");

            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            loan.Approve(Guid.NewGuid(), "Approved");

            // Loan is Approved, NOT Disbursed

            // Act
            var act = () => loan.RecordRepayment(20000000);

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void RecordRepayment_WhenAmountIsNotGreaterThanZero_ShouldThrowException(decimal amount)
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            loan.Approve(Guid.NewGuid(), "Approved");
            loan.Disburse();

            // Act
            var act = () => loan.RecordRepayment(amount);

            // Assert
            act.Should().Throw<ArgumentException>();

            loan.Status.Should().Be(LoanStatus.Disbursed);
        }
        
        [Fact]
        public void RecordRepayment_WhenTotalRepaymentExceedsLoanAmount_ShouldThrowException()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            loan.Approve(Guid.NewGuid(), "Approved");
            loan.Disburse();

            loan.RecordRepayment(80_000_000);

            // Act
            var act = () => loan.RecordRepayment(30_000_000);

            // Assert
            act.Should().Throw<InvalidOperationException>();
            loan.TotalRepaid.Should().Be(80_000_000);
        }

        [Fact]
        public void RecordRepayment_WhenPartialPayment_ShouldChangeStatusToRepaying()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            loan.Approve(Guid.NewGuid(), "Approved");
            loan.Disburse();

            // Act
            loan.RecordRepayment(30_000_000);

            // Assert
            loan.Status.Should().Be(LoanStatus.Repaying);
            loan.TotalRepaid.Should().Be(30_000_000);
        }
        [Fact]
        public void RecordRepayment_WhenFullPayment_ShouldChangeStatusToCompleted()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            loan.Approve(Guid.NewGuid(), "Approved");
            loan.Disburse();

            // Act
            loan.RecordRepayment(100_000_000);

            // Assert
            loan.Status.Should().Be(LoanStatus.Completed);
            loan.TotalRepaid.Should().Be(100_000_000);
        }
        [Fact]
        public void RecordRepayment_WhenRemainingAmountIsFullyPaid_ShouldChangeStatusToCompleted()
        {
            // Arrange
            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");
            loan.Submit();
            loan.StartReview(Guid.NewGuid());
            loan.Approve(Guid.NewGuid(), "Approved");
            loan.Disburse();

            loan.RecordRepayment(60_000_000);

            loan.Status.Should().Be(LoanStatus.Repaying);
            loan.TotalRepaid.Should().Be(60_000_000);

            // Act
            loan.RecordRepayment(40_000_000);

            // Assert
            loan.Status.Should().Be(LoanStatus.Completed);
            loan.TotalRepaid.Should().Be(100_000_000);
        }
        #endregion
    }
}
