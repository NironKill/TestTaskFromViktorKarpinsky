using FluentValidation;
using IndependentTree.Application.Enums;
using IndependentTree.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Trees.Write.Update
{
    public class UpdateTreeValidator : AbstractValidator<UpdateTreeRequest>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTreeValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.Name).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Name).Length(3, 20).WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Name).MustAsync(UniqueName)
                .WithMessage("Name must be unique")
                .WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Id).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Id).MustAsync(TreeExists)
                .WithMessage("There's no such tree.")
                .WithErrorCode($"{StatuseCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> UniqueName(UpdateTreeRequest request, string name, CancellationToken cancellation)
            => !await _context.Tree.AnyAsync(x => x.Name == name);

        private async Task<bool> TreeExists(UpdateTreeRequest request, Guid treeId, CancellationToken cancellation) 
            => await _context.Tree.AnyAsync(x => x.Id == treeId);
    }
}
