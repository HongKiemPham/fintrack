using FinTrack.Domain.Common;
using FinTrack.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinTrack.Domain.Entities
{
    public class Approval: AuditableEntity
    {
        //private readonly HashSet<Guid> _approverIds = [];
        private readonly List<ApprovalDecision> _decisions = [];
        public IReadOnlyCollection<ApprovalDecision> Decisions =>
            _decisions.AsReadOnly();

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

        public void Approve(Guid approverId)
        {
            if (approverId == Guid.Empty)
                throw new ArgumentException(
                    "ApproverId is required.",
                    nameof(approverId));

            if (Status != ApprovalStatus.Pending)
                throw new InvalidOperationException(
                    "Only pending approval can be approved.");

            if (_decisions.Any(x =>
                x.UserId == approverId &&
                x.Decision == ApprovalDecisionType.Approved))
                throw new InvalidOperationException(
                    "The same approver cannot approve twice.");

            _decisions.Add(
                new ApprovalDecision(
                    approverId,
                    ApprovalDecisionType.Approved,
                    null));

            if (_decisions.Count(x =>
                    x.Decision == ApprovalDecisionType.Approved)
                >= RequiredApprovals)
            {
                Status = ApprovalStatus.Approved;
            }
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

            _decisions.Add(
                new ApprovalDecision(
                    rejectorId,
                    ApprovalDecisionType.Rejected,
                    reason));

            Status = ApprovalStatus.Rejected;
        }
    }
}
