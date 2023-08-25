using LanchesMac.Models;

namespace LanchesMac.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<lanche> Lanches { get; }

        IEnumerable<lanche> LanchesPreferidos { get; }

        lanche GetLancheById(int lancheId);

    }
}
