namespace FilaRapida.Models
{
    public class Pedido
    {
        public int UsuarioId { get; set; }
        public decimal Total { get; set; }

        public List<PedidoItem> Itens { get; set; } = new();
    }
}
