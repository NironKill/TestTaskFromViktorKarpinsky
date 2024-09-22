using IndependentTree.Domain;

namespace IndependentTree.Application.Requests.Nodes.Read.GetAll
{
    public class GetAllNodeResponce
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TreeId { get; set; }
        public Guid ParentId { get; set; }

        public ICollection<GetAllNodeResponce> Nodes { get; set; }
    }
}
