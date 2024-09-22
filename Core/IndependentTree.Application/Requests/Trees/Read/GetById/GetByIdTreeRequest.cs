using MediatR;

namespace IndependentTree.Application.Requests.Trees.Read.GetById
{
    public class GetByIdTreeRequest : IRequest<GetByIdTreeResponce>
    {
        public Guid TreeId { get; set; }
    }
}
