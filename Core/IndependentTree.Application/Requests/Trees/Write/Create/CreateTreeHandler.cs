using AutoMapper;
using IndependentTree.Application.Interfaces;
using IndependentTree.Application.Models;
using IndependentTree.Domain;
using MediatR;

namespace IndependentTree.Application.Requests.Trees.Write.Create
{
    public class CreateTreeHandler : IRequestHandler<CreateTreeRequest, CreateTreeResponce>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateTreeHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CreateTreeResponce> Handle(CreateTreeRequest request, CancellationToken cancellationToken)
        {
            Tree newTree = new Tree
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
            };
            await _context.Tree.AddAsync(newTree, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            Node originalNode = new Node
            {
                Id= Guid.NewGuid(),
                Name = "Original Node",
                ParentId = Guid.Empty,
                TreeId = newTree.Id,
            };
            await _context.Node.AddAsync(originalNode, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            CreateTreeResponce responce = new CreateTreeResponce
            {
                Id = newTree.Id,
                Name = newTree.Name,
                OriginalNode = _mapper.Map<NodeDTO>(originalNode)
            };
            return responce;
        }
    }
}
