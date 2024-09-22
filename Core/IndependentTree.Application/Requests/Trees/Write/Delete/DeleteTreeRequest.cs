using MediatR;

namespace IndependentTree.Application.Requests.Trees.Write.Delete
{
    public class DeleteTreeRequest : IRequest<DeleteTreeResponce>
    {
        public Guid Id { get; set; }
    }
}
