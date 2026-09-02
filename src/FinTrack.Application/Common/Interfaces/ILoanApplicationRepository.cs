using FinTrack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinTrack.Application.Common.Interfaces
{
    public interface ILoanApplicationRepository
    {
        Task<LoanApplication?> GetByIdAsync(
            Guid loanId,
            CancellationToken cancellationToken = default);

        void Update(LoanApplication loan);
    }
}
