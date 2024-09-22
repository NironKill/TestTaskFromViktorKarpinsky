using System.ComponentModel.DataAnnotations;

namespace IndependentTree.Application.Requests.Trees.Write.Update
{
    public class UpdateTreeResponce
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
