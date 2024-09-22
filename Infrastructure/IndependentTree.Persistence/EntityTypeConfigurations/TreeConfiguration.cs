using IndependentTree.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IndependentTree.Persistence.EntityTypeConfigurations
{
    public class TreeConfiguration : IEntityTypeConfiguration<Tree>
    {
        public void Configure(EntityTypeBuilder<Tree> builder) => builder.HasMany(t => t.Nodes).WithOne(n => n.Tree).HasForeignKey(n => n.TreeId);     
    }
}
