using System.ComponentModel.DataAnnotations;

namespace IndependentTree.Application.Requests.Nodes.Write.Create
{
    public class CreateNodeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TreeId { get; set; }
        public Guid ParentId { get; set; }
    }
}
