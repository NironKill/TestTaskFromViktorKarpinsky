namespace IndependentTree.Application.Requests.Trees.Read.GetAll
{
    public class GetAllTreeViewNode
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TreeId { get; set; }
        public Guid ParentId { get; set; }

        public ICollection<GetAllTreeViewNode> Nodes { get; set; }
    }
}
