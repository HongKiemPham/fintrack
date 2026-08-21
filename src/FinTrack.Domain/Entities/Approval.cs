using System;
using System.Collections.Generic;
using System.Text;
using FinTrack.Domain.Enums;

namespace FinTrack.Domain.Entities
{
    public class Approval
    {
        private readonly HashSet<Guid> _approverIds = [];

        private Approval()
        {
        }

        public Approval(int requiredApprovals)
        {
            if (requiredApprovals <= 0)
                throw new ArgumentException(
                    "Required approvals must be greater than zero.",
                    nameof(requiredApprovals));

            RequiredApprovals = requiredApprovals;
            Status = ApprovalStatus.Pending;
        }

        public int RequiredApprovals { get; private set; }

        public ApprovalStatus Status { get; private set; }

        public IReadOnlyCollection<Guid> ApproverIds =>
            _approverIds;

        public void Approve(Guid approverId)
        {
            if (approverId == Guid.Empty)
                throw new ArgumentException(
                    "ApproverId is required.",
                    nameof(approverId));

            if (Status == ApprovalStatus.Approved)
                throw new InvalidOperationException(
                    "Approval has already been approved.");

            if (Status != ApprovalStatus.Pending)
                throw new InvalidOperationException(
                    "Only pending approval can be approved.");

            if (!_approverIds.Add(approverId))
                throw new InvalidOperationException(
                    "The same approver cannot approve twice.");

            if (_approverIds.Count >= RequiredApprovals)
                Status = ApprovalStatus.Approved;
        }
        public void Reject(Guid rejectorId, string? reason)
        {
            if (rejectorId == Guid.Empty)
                throw new ArgumentException(
                    "RejectorId is required.",
                    nameof(rejectorId));

            if (Status != ApprovalStatus.Pending)
                throw new InvalidOperationException(
                    "Only pending approval can be rejected.");

            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException(
                    "Reject reason is required.",
                    nameof(reason));

            Status = ApprovalStatus.Rejected;
        }
    }
}
