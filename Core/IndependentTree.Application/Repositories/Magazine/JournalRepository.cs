using AutoMapper;
using IndependentTree.Application.Interfaces;
using IndependentTree.Application.Models;
using IndependentTree.Domain;
using MediatR;

namespace IndependentTree.Application.Repositories.Magazine
{
    public class JournalRepository : IJournalRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public JournalRepository(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(JournalDTO dto, CancellationToken cancellationToken)
        {
            Journal newLog = _mapper.Map<Journal>(dto);

            await _context.Journal.AddAsync(newLog);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
