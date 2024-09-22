using AutoMapper;
using IndependentTree.Application.Common.Mappings;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IndependentTree.Application.Requests.Nodes.Write.Create
{
    public class CreateNodeDTO : IMapWith<CreateNodeRequest>
    {
        public string Name { get; set; }
        public Guid TreeId { get; set; }
        public Guid ParentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNodeDTO, CreateNodeRequest>();
        }
    }
}
