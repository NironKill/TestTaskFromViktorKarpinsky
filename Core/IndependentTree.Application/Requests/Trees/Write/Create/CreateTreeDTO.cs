using AutoMapper;
using IndependentTree.Application.Common.Mappings;
using IndependentTree.Domain;

namespace IndependentTree.Application.Requests.Trees.Write.Create
{
    public class CreateTreeDTO : IMapWith<CreateTreeRequest>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTreeDTO, CreateTreeRequest>();
        }
    }
}
