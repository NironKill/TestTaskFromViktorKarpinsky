using IndependentTree.Application.Models;

namespace IndependentTree.Application.Requests.Trees.Write.Delete
{
    public class DeleteTreeResponce
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<NodeDTO> Nodes { get; set; }
    }
}
