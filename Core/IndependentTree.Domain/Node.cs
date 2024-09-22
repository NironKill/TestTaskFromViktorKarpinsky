using System.ComponentModel.DataAnnotations;

namespace IndependentTree.Domain
{
    public class Node
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid TreeId { get; set; }

        public Guid ParentId { get; set; }

        public Tree Tree { get; set; }
    }
}
