using FluentValidation;
using IndependentTree.Application.Enums;
using IndependentTree.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Nodes.Write.Create
{
    public class CreateNodeValidator : AbstractValidator<CreateNodeRequest>
    {
        private readonly IApplicationDbContext _context;

        public CreateNodeValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.Name).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Name).Length(3, 20).WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Name).MustAsync(UniqueName)
                .WithMessage("Name must be unique")
                .WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.ParentId).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.ParentId).MustAsync(ParentExists)
                .WithMessage("The parent does not exist in this tree")
                .WithErrorCode($"{StatuseCode.NotFound.GetHashCode()}");

            RuleFor(request => request.TreeId).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");
        }

        private async Task<bool> UniqueName(CreateNodeRequest request, string name, CancellationToken cancellation) 
            => !await _context.Node.Where(x => x.TreeId == request.TreeId).AnyAsync(x => x.Name == name);

        private async Task<bool> ParentExists(CreateNodeRequest request, Guid parentId, CancellationToken cancellation) 
            => await _context.Node.Where(x => x.TreeId == request.TreeId).AnyAsync(x => x.Id == parentId);
    }
}
