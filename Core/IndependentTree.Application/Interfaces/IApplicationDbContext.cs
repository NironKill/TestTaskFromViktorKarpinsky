using IndependentTree.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IndependentTree.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Node> Node { get; set; }
        DbSet<Tree> Tree { get; set; }
        DbSet<Journal> Journal { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
