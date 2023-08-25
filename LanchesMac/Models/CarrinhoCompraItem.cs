using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LanchesMac.Models
{
    [Table("CarrinhoCompraItem")]
    public class CarrinhoCompraItem
    {

        public int CarrinhoCompraItemId { get; set; }    

        public lanche lanche { get; set; }  

        public int Quantidade { get; set; }

        [StringLength(200)] 
        public string CarrinhoCompraId { get; set; }    
    }
}
