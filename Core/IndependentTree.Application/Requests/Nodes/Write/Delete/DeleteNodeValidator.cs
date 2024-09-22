using FluentValidation;
using IndependentTree.Application.Enums;
using IndependentTree.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Nodes.Write.Delete
{
    public class DeleteNodeValidator : AbstractValidator<DeleteNodeRequest>
    {
        private readonly IApplicationDbContext _context;

        public DeleteNodeValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.NodeId).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.NodeId).MustAsync(NodeExists)
                .WithMessage("The node does not exist in this tree")
                .WithErrorCode($"{StatuseCode.NotFound.GetHashCode()}");

            RuleFor(request => request.TreeId).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");
        }

        private async Task<bool> NodeExists(DeleteNodeRequest request, Guid NodeId, CancellationToken cancellation) =>
            await _context.Node.Where(x => x.TreeId == request.TreeId).AnyAsync(x => x.Id == NodeId);
    }
}
