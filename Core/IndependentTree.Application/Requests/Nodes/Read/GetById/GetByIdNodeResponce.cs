using IndependentTree.Application.Requests.Nodes.Read.GetAll;
using System.ComponentModel.DataAnnotations;

namespace IndependentTree.Application.Requests.Nodes.Read.GetById
{
    public class GetByIdNodeResponce
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TreeId { get; set; }
        public Guid ParentId { get; set; }

        public ICollection<GetByIdNodeResponce> Nodes { get; set; }
    }
}
