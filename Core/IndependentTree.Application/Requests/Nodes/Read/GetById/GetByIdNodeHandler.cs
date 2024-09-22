using IndependentTree.Application.Interfaces;
using IndependentTree.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Nodes.Read.GetById
{
    public class GetByIdNodeHandler : IRequestHandler<GetByIdNodeRequest, GetByIdNodeResponce>
    {
        private readonly IApplicationDbContext _context;

        public GetByIdNodeHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetByIdNodeResponce> Handle(GetByIdNodeRequest request, CancellationToken cancellationToken)
        {
            Node rootNode = await _context.Node.Where(x => x.TreeId == request.TreeId).FirstOrDefaultAsync(x => x.Id == request.NodeId);
            List<Node> listNodes = await _context.Node.Where(x => x.TreeId == request.TreeId).ToListAsync();

            Dictionary<Guid, GetByIdNodeResponce> responce = listNodes.ToDictionary(n => n.Id, n => new GetByIdNodeResponce
            {
                Id = n.Id,
                Name = n.Name,
                TreeId = n.TreeId,
                ParentId = n.ParentId,
                Nodes = new List<GetByIdNodeResponce>()
            });

            foreach (Node node in listNodes)
            {
                if (node.ParentId != rootNode.ParentId && responce.ContainsKey(node.ParentId))
                    responce[node.ParentId].Nodes.Add(responce[node.Id]);
            }        
            return responce[rootNode.Id];
        }
    }
}
