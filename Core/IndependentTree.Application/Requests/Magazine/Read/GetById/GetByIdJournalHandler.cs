using AutoMapper;
using IndependentTree.Application.Interfaces;
using IndependentTree.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Magazine.Read.GetById
{
    public class GetByIdJournalHandler : IRequestHandler<GetByIdJournalRequest, GetByIdJournalResponce>
    {
        private readonly IApplicationDbContext _context;

        public GetByIdJournalHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetByIdJournalResponce> Handle(GetByIdJournalRequest request, CancellationToken cancellationToken)
        {
            Journal log = await _context.Journal.FirstOrDefaultAsync(x => x.ExceptionId == request.ExceptionId);

            GetByIdJournalResponce responce = new GetByIdJournalResponce
            {
                ExceptionId = log.ExceptionId,
                QueryParameters = log.QueryParameters,
                BodyParameters = log.BodyParameters,
                StackTrace = log.StackTrace,
                StatusCode = log.StatusCode,
                Timestamp = log.Timestamp,
                TypeRequest = log.TypeRequest,
            };
            return responce;
        }
    }
}
