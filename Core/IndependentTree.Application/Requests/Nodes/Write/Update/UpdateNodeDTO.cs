using AutoMapper;
using IndependentTree.Application.Common.Mappings;
using IndependentTree.Application.Requests.Nodes.Write.Create;

namespace IndependentTree.Application.Requests.Nodes.Write.Update
{
    public class UpdateNodeDTO : IMapWith<UpdateNodeRequest>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateNodeDTO, UpdateNodeRequest>();
        }
    }
}
