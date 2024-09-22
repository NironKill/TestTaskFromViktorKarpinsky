using AutoMapper;
using AutoMapper.QueryableExtensions;
using IndependentTree.Application.Interfaces;
using IndependentTree.Application.Models;
using IndependentTree.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Trees.Write.Delete
{
    public class DeleteTreeHandler : IRequestHandler<DeleteTreeRequest, DeleteTreeResponce>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteTreeHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DeleteTreeResponce> Handle(DeleteTreeRequest request, CancellationToken cancellationToken)
        {
            Tree tree = await _context.Tree.FirstOrDefaultAsync(x => x.Id == request.Id);

            List<NodeDTO> nodes = await _context.Node
                .Where(x => x.TreeId == tree.Id)
                .ProjectTo<NodeDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            _context.Tree.Remove(tree);
            await _context.SaveChangesAsync(cancellationToken);

            DeleteTreeResponce responce = new DeleteTreeResponce
            {
                Id = tree.Id,
                Name = tree.Name,
                Nodes = nodes
            };
            return responce;
        }
    }
}
