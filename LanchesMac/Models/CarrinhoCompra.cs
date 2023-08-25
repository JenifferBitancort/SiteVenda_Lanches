using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;



namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {

        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; } 
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
        public static CarrinhoCompra GetCarrinho(IServiceProvider services) 
        {
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;   


            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();  

            session.SetString("CarrinhoId", carrinhoId); 

            var context = services.GetService<AppDbContext>();  

            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId 
            };

        }

        public void AdicionarAoCarrinho(lanche lanche) 
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                     s => s.lanche.LancheId == lanche.LancheId &&
                     s.CarrinhoCompraId == CarrinhoCompraId);


            if (carrinhoCompraItem == null) 
            {
                carrinhoCompraItem = new CarrinhoCompraItem 
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    lanche = lanche,
                    Quantidade = 1
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem); 

            }
            else 
            {
                carrinhoCompraItem.Quantidade++; 
            }
            _context.SaveChanges(); 
        }

        public int RemoverDoCarrinho(lanche lanche) 
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
            s => s.lanche.LancheId == lanche.LancheId &&
                   s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;    

            if (carrinhoCompraItem != null) 
            {
                if (carrinhoCompraItem.Quantidade > 1)  
                {
                    carrinhoCompraItem.Quantidade--; 
                    quantidadeLocal = carrinhoCompraItem.Quantidade;  
                }
                else 
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);    
                }
            }
            _context.SaveChanges(); 
            return quantidadeLocal; 
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()    
        {
            return CarrinhoCompraItens ??   
                   (CarrinhoCompraItens =
                       _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                           .Include(s => s.lanche)
                           .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                                 .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);
            
            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges(); 
        }

        public decimal GetCarrinhoCompraTotal() 
        {
            var total = _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.lanche.Preço * c.Quantidade).Sum();
            return total;   
        }
    }



    
}
