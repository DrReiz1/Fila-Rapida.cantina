using FilaRapida.Data;
using FilaRapida.Models;

namespace FilaRapida.Services
{
    public class ProdutoService
    {
        public void CriarProduto(string nome, decimal preco, int qtd, int min, string perfil)
        {
            if (perfil != "ADMIN")
                throw new Exception("Somente ADMIN");

            ProdutoRepo.Criar(nome, preco, qtd, min);
        }


        public List<ProdutoDto> Listar(string perfil)
        {
            if (perfil == "ADMIN")
                return ProdutoRepo.ListarTodos();

            return ProdutoRepo.ListarAtivos();
        }

        public void AlterarPreco(int id, decimal preco, string perfil)
        {
            if (perfil != "ADMIN")
                throw new Exception("Somente ADMIN pode alterar.");

            ProdutoRepo.AlterarPreco(id, preco);
        }

        public void Inativar(int id, string perfil)
        {
            if (perfil != "ADMIN")
                throw new Exception("Somente ADMIN pode inativar.");

            ProdutoRepo.Inativar(id);
        }

        public void Ativar(int id, string perfil)
        {
            if (perfil != "ADMIN")
                throw new Exception("Somente ADMIN pode ativar.");

            ProdutoRepo.Ativar(id);
        }

        public void Consumir(int id, int qtd)
        {
            var prod = ProdutoRepo.Buscar(id);

            if (prod.Quantidade < qtd)
                throw new Exception("Sem estoque");

            ProdutoRepo.AlterarQuantidade(id, prod.Quantidade - qtd);
        }

        public void Vender(int id, int qtd)
        {
            var prod = ProdutoRepo.Buscar(id);

            if (prod == null)
                throw new Exception("Produto não existe");

            if (prod.Quantidade < qtd)
                throw new Exception("Estoque insuficiente");

            ProdutoRepo.AlterarQuantidade(id, prod.Quantidade - qtd);
        }


    }
}
