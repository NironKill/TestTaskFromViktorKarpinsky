using MediatR;

namespace IndependentTree.Application.Requests.Trees.Write.Update
{
    public class UpdateTreeRequest : IRequest<UpdateTreeResponce>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
