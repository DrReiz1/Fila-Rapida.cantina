namespace FilaRapida.Models
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
        public int Quantidade { get; set; }
        public int EstoqueMin { get; set; }
    }
}