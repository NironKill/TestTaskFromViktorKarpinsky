using FluentValidation;
using IndependentTree.Application.Enums;
using IndependentTree.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Trees.Write.Delete
{
    public class DeleteTreeValidator : AbstractValidator<DeleteTreeRequest>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTreeValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.Id).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Id).MustAsync(TreeExists)
                .WithMessage("There's no such tree.")
                .WithErrorCode($"{StatuseCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> TreeExists(DeleteTreeRequest request, Guid treeId, CancellationToken cancellation) 
            => await _context.Tree.AnyAsync(x => x.Id == treeId);
    }
}
