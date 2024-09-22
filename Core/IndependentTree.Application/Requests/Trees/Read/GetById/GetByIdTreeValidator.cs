using FluentValidation;
using IndependentTree.Application.Enums;
using IndependentTree.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Trees.Read.GetById
{
    public class GetByIdTreeValidator : AbstractValidator<GetByIdTreeRequest>
    {
        private readonly IApplicationDbContext _context;

        public GetByIdTreeValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.TreeId).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.TreeId).MustAsync(TreeExists)
                .WithMessage("There's no such tree.")
                .WithErrorCode($"{StatuseCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> TreeExists(GetByIdTreeRequest request, Guid TreeId, CancellationToken cancellation) 
            => await _context.Tree.AnyAsync(x => x.Id == TreeId);
    }
}
