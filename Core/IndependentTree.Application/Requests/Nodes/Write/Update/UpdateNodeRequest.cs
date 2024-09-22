using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IndependentTree.Application.Requests.Nodes.Write.Update
{
    public class UpdateNodeRequest : IRequest<UpdateNodeResponce>
    {
        public Guid NodeId { get; set; }
        public Guid TreeId { get; set; }
        public string Name { get; set; }
    }
}
