namespace IndependentTree.Application.Requests.Trees.Read.GetAll
{
    public class GetAllTreeResponce
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<GetAllTreeViewNode> Nodes { get; set; }
    }
}
