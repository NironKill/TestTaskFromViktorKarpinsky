using IndependentTree.Application.Interfaces;
using IndependentTree.Domain;
using MediatR;

namespace IndependentTree.Application.Requests.Nodes.Write.Create
{
    public class CreateNodeHandler : IRequestHandler<CreateNodeRequest, CreateNodeResponse>
    {
        private readonly IApplicationDbContext _context;

        public CreateNodeHandler(IApplicationDbContext context) => _context = context; 
        
        public async Task<CreateNodeResponse> Handle(CreateNodeRequest request, CancellationToken cancellationToken)
        {       
            Node newNode = new Node
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                TreeId = request.TreeId,
                ParentId = request.ParentId
            };

            await _context.Node.AddAsync(newNode, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            CreateNodeResponse response = new CreateNodeResponse
            {
                Id = newNode.Id,
                Name = newNode.Name,
                TreeId = newNode.TreeId,
                ParentId = newNode.ParentId
            };

            return response;
        }
    }
}
