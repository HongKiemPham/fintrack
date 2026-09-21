using FinTrack.Domain.Entities;
using FinTrack.Domain.Enums;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinTrack.Domain.Tests
{
    public class ApprovalTests
    {

        [Fact]
        public void Approval_Should_Have_An_Id()
        {
            var approval = new Approval(requiredApprovals: 2);

            approval.Id.Should().NotBe(Guid.Empty);
        }

        #region Approve
        [Fact]
        public void Approval_Should_Be_Approved_When_Required_Number_Of_Approvals_Is_Reached()
        {
            // Arrange
            var approval = new Approval(requiredApprovals: 2);

            // Act - first approval
            approval.Approve(Guid.NewGuid());

            // Assert
            approval.Status.Should().Be(ApprovalStatus.Pending);

            // Act - second approval
            approval.Approve(Guid.NewGuid());

            // Assert
            approval.Status.Should().Be(ApprovalStatus.Approved);
        }

        [Fact]
        public void Approval_Should_Remain_Pending_When_Required_Number_Of_Approvals_Is_Not_Reached()
        {
            // Arrange
            var approval = new Approval(requiredApprovals: 3);

            // Act
            approval.Approve(Guid.NewGuid());
            approval.Approve(Guid.NewGuid());

            // Assert
            approval.Status.Should().Be(ApprovalStatus.Pending);
        }

        [Fact]
        public void Approval_Should_Throw_When_Same_Approver_Approves_Twice()
        {
            // Arrange
            var approval = new Approval(requiredApprovals: 2);
            var approverId = Guid.NewGuid();

            approval.Approve(approverId);

            // Act
            var act = () => approval.Approve(approverId);

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Approval_Should_Throw_When_RequiredApprovals_Is_Not_Greater_Than_Zero(
    int requiredApprovals)
        {
            // Act
            var act = () => new Approval(requiredApprovals);

            // Assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Approve_WhenApprovalIsRejected_ShouldThrowException()
        {
            // Arrange
            var approval = new Approval(requiredApprovals: 2);

            approval.Reject(
                Guid.NewGuid(),
                "Risk is too high");

            // Act
            var act = () => approval.Approve(Guid.NewGuid());

            // Assert
            act.Should().Throw<InvalidOperationException>();

            approval.Status.Should().Be(ApprovalStatus.Rejected);
        }
        #endregion

        #region Reject
        [Fact]
        public void Approval_Should_Be_Rejected_When_Rejected()
        {
            // Arrange
            var approval = new Approval(requiredApprovals: 2);

            // Act
            approval.Reject(Guid.NewGuid(), "Risk is too high");

            // Assert
            approval.Status.Should().Be(ApprovalStatus.Rejected);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Reject_WhenReasonIsEmpty_ShouldThrowException(string? reason)
        {
            // Arrange
            var approval = new Approval(requiredApprovals: 2);

            // Act
            var act = () => approval.Reject(Guid.NewGuid(), reason);

            // Assert
            act.Should().Throw<ArgumentException>();

            approval.Status.Should().Be(ApprovalStatus.Pending);
        }
        [Fact]
        public void Reject_Should_Record_Rejection_Information()
        {
            // Arrange
            var approval = new Approval(2);
            var rejectorId = Guid.NewGuid();

            // Act
            approval.Reject(rejectorId, "Risk is too high");

            // Assert
            var decision = approval.Decisions.Single();

            decision.UserId.Should().Be(rejectorId);
            decision.Decision.Should().Be(
                ApprovalDecisionType.Rejected);
            decision.Reason.Should().Be("Risk is too high");
        }
        #endregion

        [Fact]
        public async Task Handle_Should_Keep_Loan_UnderReview_When_Approval_Is_Not_Completed()
        {
            // Arrange
            var loanId = Guid.NewGuid();
            var approverId = Guid.NewGuid();

            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");

            loan.Submit();
            loan.StartReview(Guid.NewGuid());

            var approval = new Approval(requiredApprovals: 2);

            loan.AttachApproval(approval.Id);

            // TODO:
            // setup repository mocks
            // create handler

            // Act
            // await handler.HandleAsync(...);

            // Assert
            loan.Status.Should().Be(LoanStatus.UnderReview);
            approval.Status.Should().Be(ApprovalStatus.Pending);
        }
        [Fact]
        public async Task Handle_Should_Approve_Loan_When_Approval_Is_Completed()
        {
            // Arrange
            var loanId = Guid.NewGuid();
            var approver1 = Guid.NewGuid();
            var approver2 = Guid.NewGuid();

            var loan = new LoanApplication(
                Guid.NewGuid(),
                100_000_000,
                12,
                10,
                "Personal");

            loan.Submit();
            loan.StartReview(Guid.NewGuid());

            var approval = new Approval(requiredApprovals: 2);

            loan.AttachApproval(approval.Id);

            // Act
            // approver1
            // approver2

            // Assert
            loan.Status.Should().Be(LoanStatus.Approved);
            approval.Status.Should().Be(ApprovalStatus.Approved);
        }
    }
}
