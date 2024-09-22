using MediatR;

namespace IndependentTree.Application.Requests.Nodes.Write.Create
{
    public record CreateNodeRequest : IRequest<CreateNodeResponse>
    {
        public string Name { get; set; }
        public Guid TreeId { get; set; }
        public Guid ParentId { get; set; }
    }
}
