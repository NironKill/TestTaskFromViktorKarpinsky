using IndependentTree.Application.Interfaces;
using IndependentTree.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Trees.Read.GetById
{
    public class GetByIdTreeHandler : IRequestHandler<GetByIdTreeRequest, GetByIdTreeResponce>
    {
        private readonly IApplicationDbContext _context;

        public GetByIdTreeHandler(IApplicationDbContext context) => _context = context;
        
        public async Task<GetByIdTreeResponce> Handle(GetByIdTreeRequest request, CancellationToken cancellationToken)
        {
            Tree tree = await _context.Tree.FirstOrDefaultAsync(x => x.Id == request.TreeId);

            List<Node> listNodes = await _context.Node.Where(x => x.TreeId == request.TreeId).ToListAsync(cancellationToken);

            Dictionary<Guid, GetByIdTreeViewNode> nodeLookup = listNodes.ToDictionary(n => n.Id, n => new GetByIdTreeViewNode
            {
                Id = n.Id,
                Name = n.Name,
                TreeId = n.TreeId,
                ParentId = n.ParentId,
                Nodes = new List<GetByIdTreeViewNode>()
            });

            GetByIdTreeResponce response = new GetByIdTreeResponce
            {
                Id = tree.Id,
                Name = tree.Name,
                Nodes = new List<GetByIdTreeViewNode>()
            };

            foreach (var node in listNodes)
            {
                if (node.ParentId == Guid.Empty) 
                {
                    response.Nodes.Add(nodeLookup[node.Id]);
                }
                else if (nodeLookup.ContainsKey(node.ParentId)) 
                {
                    nodeLookup[node.ParentId].Nodes.Add(nodeLookup[node.Id]);
                }
            }
            return response;
        }
    }
}
