using MediatR;

namespace IndependentTree.Application.Requests.Nodes.Write.Delete
{
    public class DeleteNodeRequest : IRequest<DeleteNodeResponse>
    {
        public Guid NodeId { get; set; }
        public Guid TreeId { get; set; }
    }
}
