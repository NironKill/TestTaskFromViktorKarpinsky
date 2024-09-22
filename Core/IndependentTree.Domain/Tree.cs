using System.ComponentModel.DataAnnotations;

namespace IndependentTree.Domain
{
    public class Tree
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Node> Nodes { get; set; }
    }
}
