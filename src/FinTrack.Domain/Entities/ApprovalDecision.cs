using System;
using System.Collections.Generic;
using System.Text;
using FinTrack.Domain.Enums;

namespace FinTrack.Domain.Entities
{
    public class ApprovalDecision
    {
        private ApprovalDecision()
        {
        }

        internal ApprovalDecision(
            Guid userId,
            ApprovalDecisionType decision,
            string? reason)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException(
                    "UserId is required.",
                    nameof(userId));

            if (decision == ApprovalDecisionType.Rejected &&
                string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException(
                    "Reject reason is required.",
                    nameof(reason));

            Id = Guid.NewGuid();
            UserId = userId;
            Decision = decision;
            Reason = reason;
            DecidedAt = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public ApprovalDecisionType Decision { get; private set; }

        public string? Reason { get; private set; }

        public DateTimeOffset DecidedAt { get; private set; }
    }
}
