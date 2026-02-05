namespace FilaRapida.Models
{
    public class PedidoRepoDto
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public string Status { get; set; }
    }
}
