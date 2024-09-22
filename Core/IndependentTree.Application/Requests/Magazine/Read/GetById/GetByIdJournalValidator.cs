using FluentValidation;
using IndependentTree.Application.Enums;
using IndependentTree.Application.Interfaces;
using IndependentTree.Application.Requests.Nodes.Read.GetById;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Magazine.Read.GetById
{
    public class GetByIdJournalValidator : AbstractValidator<GetByIdJournalRequest>
    {
        private readonly IApplicationDbContext _context;

        public GetByIdJournalValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.ExceptionId).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.ExceptionId).MustAsync(LogExists)
                .WithMessage("There is no such exception.")
                .WithErrorCode($"{StatuseCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> LogExists(GetByIdJournalRequest request, Guid exceptionId, CancellationToken cancellation)
            => await _context.Journal.AnyAsync(x => x.ExceptionId == exceptionId);
    }
}
