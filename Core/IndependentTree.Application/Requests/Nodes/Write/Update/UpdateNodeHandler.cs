using IndependentTree.Application.Interfaces;
using IndependentTree.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Nodes.Write.Update
{
    public class UpdateNodeHandler : IRequestHandler<UpdateNodeRequest, UpdateNodeResponce>
    {
        private readonly IApplicationDbContext _context;

        public UpdateNodeHandler(IApplicationDbContext context) => _context = context;

        public async Task<UpdateNodeResponce> Handle(UpdateNodeRequest request, CancellationToken cancellationToken)
        {
            Node updateNode = await _context.Node.Where(x => x.TreeId == request.TreeId).FirstOrDefaultAsync(x => x.Id == request.NodeId);

            updateNode.Name = request.Name;

            _context.Node.Update(updateNode);
            await _context.SaveChangesAsync(cancellationToken);

            UpdateNodeResponce responce = new UpdateNodeResponce
            {
                Id = updateNode.Id,
                TreeId = updateNode.TreeId,
                ParentId = updateNode.ParentId,
                Name = updateNode.Name,
            };

            return responce;
        }
    }
}
