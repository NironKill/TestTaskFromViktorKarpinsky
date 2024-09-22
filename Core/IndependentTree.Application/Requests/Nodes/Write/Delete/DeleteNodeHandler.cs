using IndependentTree.Application.Interfaces;
using IndependentTree.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Nodes.Write.Delete
{
    public class DeleteNodeHandler : IRequestHandler<DeleteNodeRequest, DeleteNodeResponse>
    {
        private readonly IApplicationDbContext _context;

        public DeleteNodeHandler(IApplicationDbContext context) => _context = context;

        public async Task<DeleteNodeResponse> Handle(DeleteNodeRequest request, CancellationToken cancellationToken)
        {
            Node firstNode = await _context.Node.Where(x => x.TreeId == request.TreeId).FirstOrDefaultAsync(x => x.Id == request.NodeId);

            if (firstNode.ParentId == Guid.Empty)
            {
                Tree tree = await _context.Tree.FirstOrDefaultAsync(x => x.Id == firstNode.TreeId);

                List<Guid> listNodeId = await _context.Node.Where(x => x.TreeId == tree.Id).Select(x => x.Id).ToListAsync();

                _context.Tree.Remove(tree);
                await _context.SaveChangesAsync(cancellationToken);

                DeleteNodeResponse responseNodeId = new DeleteNodeResponse
                {
                    RemoteNodeId = listNodeId
                };
                return responseNodeId;
            }

            List<Node> nodesToDelete = new List<Node>();
            nodesToDelete.Add(firstNode);

            List<Node> nodesToProcess = nodesToDelete;
            bool anySuccessorsLeft = true;
            while (anySuccessorsLeft)
            {
                List<Node> successors = await _context.Node
                    .Where(x => x.TreeId == firstNode.TreeId)
                    .Where(n => nodesToProcess.Select(x => x.Id).Contains(n.ParentId))
                    .ToListAsync();

                if (successors.Any())
                {
                    nodesToDelete.AddRange(successors);
                    nodesToProcess = successors;
                }
                else
                    anySuccessorsLeft = false;               
            }

            List<Guid> listId = new List<Guid>();
            foreach (Node nodeToDelete in nodesToDelete)
            {
                listId.Add(nodeToDelete.Id);
                _context.Node.Remove(nodeToDelete);
                await _context.SaveChangesAsync(cancellationToken);
            }

            DeleteNodeResponse response = new DeleteNodeResponse
            {
                RemoteNodeId = listId
            };
            return response;
        }
    }
}
