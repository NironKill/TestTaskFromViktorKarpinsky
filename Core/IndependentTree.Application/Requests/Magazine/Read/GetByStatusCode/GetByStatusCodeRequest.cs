using MediatR;

namespace IndependentTree.Application.Requests.Magazine.Read.GetByStatusCode
{
    public class GetByStatusCodeRequest : IRequest<List<GetByStatusCodeResponce>>
    {
        public int StatusCode { get; set; }
    }
}
