using AutoMapper;
using IndependentTree.Application.Common.Mappings;
using IndependentTree.Domain;

namespace IndependentTree.Application.Models
{
    public class NodeDTO : IMapWith<Node>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ParentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Node, NodeDTO>();
        }
    }
}
