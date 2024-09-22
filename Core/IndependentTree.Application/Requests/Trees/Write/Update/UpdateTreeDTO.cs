using AutoMapper;
using IndependentTree.Application.Common.Mappings;
using IndependentTree.Application.Requests.Nodes.Write.Update;

namespace IndependentTree.Application.Requests.Trees.Write.Update
{
    public class UpdateTreeDTO : IMapWith<UpdateTreeRequest>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateTreeDTO, UpdateTreeRequest>();
        }
    }
}
