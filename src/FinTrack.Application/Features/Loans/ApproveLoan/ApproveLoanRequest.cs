using System;
using System.Collections.Generic;
using System.Text;

namespace FinTrack.Application.Features.Loans.ApproveLoan
{
    public sealed record ApproveLoanRequest(
    Guid LoanId,
    Guid ApproverId);
}
