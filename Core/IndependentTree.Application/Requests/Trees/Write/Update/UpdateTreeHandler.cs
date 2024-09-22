using AutoMapper;
using IndependentTree.Application.Interfaces;
using IndependentTree.Application.Requests.Nodes.Write.Update;
using IndependentTree.Application.Requests.Trees.Write.Delete;
using IndependentTree.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Trees.Write.Update
{
    public class UpdateTreeHandler : IRequestHandler<UpdateTreeRequest, UpdateTreeResponce>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTreeHandler(IApplicationDbContext context, IMapper mapper) => _context = context;
        
        public async Task<UpdateTreeResponce> Handle(UpdateTreeRequest request, CancellationToken cancellationToken)
        {
            Tree updateTree = await _context.Tree.FirstOrDefaultAsync(x => x.Id == request.Id);

            updateTree.Name = request.Name;

            _context.Tree.Update(updateTree);
            await _context.SaveChangesAsync(cancellationToken);

            UpdateTreeResponce responce = new UpdateTreeResponce
            {
                Id = updateTree.Id,
                Name = updateTree.Name       
            };

            return responce;
        }
    }
}
