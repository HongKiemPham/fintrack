using FinTrack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinTrack.Application.Common.Interfaces
{
    public interface IApprovalRepository
    {
        Task<Approval?> GetByLoanIdAsync(
            Guid loanId,
            CancellationToken cancellationToken = default);

        void Update(Approval approval);
    }
}
