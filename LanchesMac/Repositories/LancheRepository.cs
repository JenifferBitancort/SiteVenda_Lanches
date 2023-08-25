using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;
        public LancheRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

        public IEnumerable<lanche> LanchesPreferidos => _context.Lanches.
            Where(l => l.IsLanchePreferido).Include(c => c.Categoria);
            


        public lanche GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        }
    }
}
