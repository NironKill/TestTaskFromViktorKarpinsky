using IndependentTree.Application.Interfaces;
using IndependentTree.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Magazine.Read.GetAll
{
    public class GetAllJournalHandler : IRequestHandler<GetAllJournalRequest, List<GetAllJournalResponce>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllJournalHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<GetAllJournalResponce>> Handle(GetAllJournalRequest request, CancellationToken cancellationToken)
        {
            List<Journal> listLog = await _context.Journal.ToListAsync(cancellationToken);

            List<GetAllJournalResponce> responces = new List<GetAllJournalResponce>();
            foreach (Journal log in listLog)
            {
                GetAllJournalResponce responce = new GetAllJournalResponce
                {
                    ExceptionId = log.ExceptionId,
                    Timestamp = log.Timestamp,
                    StatusCode = log.StatusCode,
                    TypeRequest = log.TypeRequest,
                    QueryParameters = log.QueryParameters,
                    BodyParameters = log.BodyParameters,
                    StackTrace = log.StackTrace,
                };
                responces.Add(responce);
            }
            return responces;
        }
    }
}
 