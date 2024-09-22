using FluentValidation;
using IndependentTree.Application.Enums;
using IndependentTree.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Trees.Write.Create
{
    public class CreateTreeValidator : AbstractValidator<CreateTreeRequest>
    {
        private readonly IApplicationDbContext _context;

        public CreateTreeValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.Name).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Name).Length(3, 20).WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Name).MustAsync(UniqueName)
                .WithMessage("Name must be unique")
                .WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");
        }

        private async Task<bool> UniqueName(CreateTreeRequest request, string name, CancellationToken cancellation)
            => !await _context.Tree.AnyAsync(x => x.Name == name);
    }
}
