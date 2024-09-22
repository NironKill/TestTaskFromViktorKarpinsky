using MediatR;

namespace IndependentTree.Application.Requests.Magazine.Read.GetById
{
    public class GetByIdJournalRequest : IRequest<GetByIdJournalResponce>
    {
        public Guid ExceptionId { get; set; }
    }
}
