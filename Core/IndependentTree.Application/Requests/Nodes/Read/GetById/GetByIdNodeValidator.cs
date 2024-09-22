using FluentValidation;
using IndependentTree.Application.Enums;
using IndependentTree.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Nodes.Read.GetById
{
    public class GetByIdNodeValidator : AbstractValidator<GetByIdNodeRequest>
    {
        private readonly IApplicationDbContext _context;

        public GetByIdNodeValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.NodeId).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.NodeId).MustAsync(NodeExists)
                .WithMessage("The node does not exist in this tree")
                .WithErrorCode($"{StatuseCode.NotFound.GetHashCode()}");

            RuleFor(request => request.TreeId).NotEmpty().WithErrorCode($"{StatuseCode.BadRequest.GetHashCode()}");
        }

        private async Task<bool> NodeExists(GetByIdNodeRequest request, Guid NodeId, CancellationToken cancellation) 
            => await _context.Node.Where(x => x.TreeId == request.TreeId).AnyAsync(x => x.Id == NodeId);
    }
}
