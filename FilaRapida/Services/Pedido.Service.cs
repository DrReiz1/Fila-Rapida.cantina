using FilaRapida.Models;
using FilaRapida.Data;

namespace FilaRapida.Services
{
    public class PedidoService
    {
        public void AdicionarItem(Pedido pedido, int produtoId, int qtd)
        {
            var produto = ProdutoRepo.Buscar(produtoId);

            if (produto == null)
                throw new Exception("Produto não existe");

            if (produto.Quantidade < qtd)
                throw new Exception("Estoque insuficiente");

            pedido.Itens.Add(new PedidoItem
            {
                ProdutoId = produtoId,
                Qtd = qtd,
                PrecoUnit = produto.Preco
            });
        }

        public void FecharPedido(Pedido pedido)
        {
            decimal total = 0;

            foreach (var i in pedido.Itens)
                total += i.Qtd * i.PrecoUnit;

            pedido.Total = total;
        }

        public void Salvar(Pedido pedido)
        {
            PedidoRepo.SalvarPedido(pedido);
        }
    }
}
