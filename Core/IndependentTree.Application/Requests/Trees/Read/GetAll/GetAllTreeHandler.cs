using IndependentTree.Application.Interfaces;
using IndependentTree.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Trees.Read.GetAll
{
    public class GetAllTreeHandler : IRequestHandler<GetAllTreeRequest, List<GetAllTreeResponce>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllTreeHandler(IApplicationDbContext context) =>_context = context;
        
        public async Task<List<GetAllTreeResponce>> Handle(GetAllTreeRequest request, CancellationToken cancellationToken)
        {
            List<Tree> listTrees = await _context.Tree.ToListAsync(cancellationToken);

            List<Node> listNodes = await _context.Node.ToListAsync(cancellationToken);

            Dictionary<Guid, List<Node>> nodesGroupedByTree = listNodes.GroupBy(x => x.TreeId).ToDictionary(x => x.Key, x => x.ToList());

            List<GetAllTreeResponce> response = new List<GetAllTreeResponce>();

            foreach (Tree tree in listTrees)
            {
                GetAllTreeResponce treeResponce = new GetAllTreeResponce
                {
                    Id = tree.Id,
                    Name = tree.Name,
                    Nodes = new List<GetAllTreeViewNode>()
                };

                if (nodesGroupedByTree.ContainsKey(tree.Id))
                {
                    List<Node> nodesInTree = nodesGroupedByTree[tree.Id];

                    Dictionary<Guid, GetAllTreeViewNode> nodeLookup = nodesInTree.ToDictionary(n => n.Id, n => new GetAllTreeViewNode
                    {
                        Id = n.Id,
                        Name = n.Name,
                        TreeId = n.TreeId,
                        ParentId = n.ParentId,
                        Nodes = new List<GetAllTreeViewNode>()
                    });

                    foreach (Node node in nodesInTree)
                    {
                        if (node.ParentId == Guid.Empty)                        
                            treeResponce.Nodes.Add(nodeLookup[node.Id]);
                        
                        else if (nodeLookup.ContainsKey(node.ParentId))                 
                            nodeLookup[node.ParentId].Nodes.Add(nodeLookup[node.Id]);                  
                    }
                }
                response.Add(treeResponce);
            }
            return response;
        }
    }
}
