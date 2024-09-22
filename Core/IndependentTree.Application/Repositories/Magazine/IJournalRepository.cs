using IndependentTree.Application.Models;

namespace IndependentTree.Application.Repositories.Magazine
{
    public interface IJournalRepository
    {
        Task Create(JournalDTO dto, CancellationToken cancellationToken);
    }
}
