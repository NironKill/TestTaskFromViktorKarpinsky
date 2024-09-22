using IndependentTree.Application.Requests.Trees.Read.GetAll;

namespace IndependentTree.Application.Requests.Trees.Read.GetById
{
    public class GetByIdTreeResponce
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<GetByIdTreeViewNode> Nodes { get; set; }
    }
}
