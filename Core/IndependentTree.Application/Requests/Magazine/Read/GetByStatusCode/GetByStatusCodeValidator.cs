using FluentValidation;
using IndependentTree.Application.Enums;
using IndependentTree.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Magazine.Read.GetByStatusCode
{
    public class GetByStatusCodeValidator : AbstractValidator<GetByStatusCodeRequest>
    {
        private readonly IApplicationDbContext _context;

        public GetByStatusCodeValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.StatusCode).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.StatusCode).MustAsync(LogExists)
                .WithMessage("There are no exceptions with this status code.")
                .WithErrorCode($"{StatuseCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> LogExists(GetByStatusCodeRequest request, int statusCode, CancellationToken cancellation)
            => await _context.Journal.AnyAsync(x => x.StatusCode == statusCode);
    }
}
