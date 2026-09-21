using FinTrack.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinTrack.Application.Features.Loans.ApproveLoan
{
    public sealed class ApproveLoanHandler
    {
        private readonly ILoanApplicationRepository _loanRepository;
        private readonly IApprovalRepository _approvalRepository;

        public ApproveLoanHandler(
            ILoanApplicationRepository loanRepository,
            IApprovalRepository approvalRepository)
        {
            _loanRepository = loanRepository;
            _approvalRepository = approvalRepository;
        }

        public async Task HandleAsync(
            ApproveLoanRequest request,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
