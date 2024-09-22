using IndependentTree.Application.Interfaces;
using IndependentTree.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IndependentTree.Application.Requests.Magazine.Read.GetByStatusCode
{
    public class GetByStatusCodeHandler : IRequestHandler<GetByStatusCodeRequest, List<GetByStatusCodeResponce>>
    {
        private readonly IApplicationDbContext _context;

        public GetByStatusCodeHandler(IApplicationDbContext context) => _context = context;
        
        public async Task<List<GetByStatusCodeResponce>> Handle(GetByStatusCodeRequest request, CancellationToken cancellationToken)
        {
            List<Journal> listLog = await _context.Journal.Where(x => x.StatusCode == request.StatusCode).ToListAsync(cancellationToken);

            List<GetByStatusCodeResponce> responces = new List<GetByStatusCodeResponce>();
            foreach (Journal log in listLog)
            {
                GetByStatusCodeResponce responce = new GetByStatusCodeResponce
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
