using IndependentTree.Application.Requests.Trees.Read.GetAll;

namespace IndependentTree.Application.Requests.Trees.Read.GetById
{
    public class GetByIdTreeViewNode
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TreeId { get; set; }
        public Guid ParentId { get; set; }

        public ICollection<GetByIdTreeViewNode> Nodes { get; set; }
    }
}