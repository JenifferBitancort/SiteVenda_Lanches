using LanchesMac.Models;

namespace LanchesMac.ViewModels
{
    public class LancheListViewModel
    {
        public IEnumerable<lanche> Lanches { get; set; }
        public string CategoriaAtual { get; set; }
    }
}
