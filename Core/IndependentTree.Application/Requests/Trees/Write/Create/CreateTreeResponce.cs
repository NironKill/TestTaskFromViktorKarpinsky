using IndependentTree.Application.Models;

namespace IndependentTree.Application.Requests.Trees.Write.Create
{
    public class CreateTreeResponce
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public NodeDTO OriginalNode { get; set; }
    }
}
