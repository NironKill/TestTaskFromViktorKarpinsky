using MediatR;

namespace IndependentTree.Application.Requests.Nodes.Read.GetById
{
    public class GetByIdNodeRequest : IRequest<GetByIdNodeResponce>
    {
        public Guid NodeId { get; set; }
        public Guid TreeId { get; set; }
    }
}
