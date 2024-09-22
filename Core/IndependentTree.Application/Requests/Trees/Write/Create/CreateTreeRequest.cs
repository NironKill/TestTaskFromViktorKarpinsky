using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IndependentTree.Application.Requests.Trees.Write.Create
{
    public class CreateTreeRequest : IRequest<CreateTreeResponce>
    {
        public string Name { get; set; }
    }
}
