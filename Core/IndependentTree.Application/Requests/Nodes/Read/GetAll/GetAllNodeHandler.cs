using IndependentTree.Application.Interfaces;
using IndependentTree.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Nodes.Read.GetAll
{
    public class GetAllNodeHandler : IRequestHandler<GetAllNodeRequest, List<GetAllNodeResponce>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllNodeHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<GetAllNodeResponce>> Handle(GetAllNodeRequest request, CancellationToken cancellationToken)
        {
            List<Node> listNodes = await _context.Node.ToListAsync(cancellationToken);

            Dictionary<Guid, List<Node>> groupedByTree = listNodes.GroupBy(x => x.TreeId).ToDictionary(x => x.Key, x => x.ToList());

            List<GetAllNodeResponce> responce = new List<GetAllNodeResponce>();

            foreach (var clusteredNodes in groupedByTree)
            {
                List<Node> nodes = clusteredNodes.Value;

                Dictionary<Guid, GetAllNodeResponce> lookup = nodes.ToDictionary(n => n.Id, n => new GetAllNodeResponce
                {
                    Id = n.Id,
                    Name = n.Name,
                    ParentId = n.ParentId,
                    TreeId = n.TreeId,
                    Nodes = new List<GetAllNodeResponce>()
                });

                List<GetAllNodeResponce> resultNodes = new List<GetAllNodeResponce>();

                foreach (Node node in nodes)
                {
                    if (node.ParentId == Guid.Empty)                  
                        resultNodes.Add(lookup[node.Id]);
                    
                    else                   
                        if (lookup.ContainsKey(node.ParentId))                  
                            lookup[node.ParentId].Nodes.Add(lookup[node.Id]);     
                }
                responce.AddRange(resultNodes);
            }
            return responce;
        }       
    }
}
