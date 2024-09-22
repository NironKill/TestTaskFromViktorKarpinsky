using AutoMapper;
using IndependentTree.Application.Common.Mappings;
using IndependentTree.Domain;

namespace IndependentTree.Application.Models
{
    public class JournalDTO : IMapWith<Journal>
    {
        public Guid ExceptionId { get; set; }
        public int Timestamp { get; set; }
        public int StatusCode { get; set; }
        public string TypeRequest { get; set; }
        public string? QueryParameters { get; set; }
        public string? BodyParameters { get; set; }
        public string StackTrace { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JournalDTO, Journal>();
        }
    }
}
