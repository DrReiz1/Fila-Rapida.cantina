using FilaRapida.Services;
using FilaRapida.Data;
using FilaRapida.Models;

namespace FilaRapida
{
    class Program
    {
        static void Main()
        {
            var auth = new AuthService();

            Console.WriteLine("========== LOGIN ==========");

            Console.Write("Login: ");
            string login = Console.ReadLine();

            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            var user = auth.Login(login, senha);

            if (user == null)
            {
                Console.WriteLine("Login inválido.");
                return;
            }

            Console.WriteLine($"Bem-vindo {user.Nome} ({user.Perfil})");

            var produtoService = new ProdutoService();
            var pedidoService = new PedidoService();

            while (true)
            {
                Console.WriteLine("\n===== MENU =====");

                if (user.Perfil == "ADMIN")
                {
                    Console.WriteLine("1 - Cadastrar produto");
                    Console.WriteLine("2 - Listar produtos");
                    Console.WriteLine("3 - Alterar preço");
                    Console.WriteLine("4 - Inativar produto");
                    Console.WriteLine("5 - Ativar produto");
                    Console.WriteLine("6 - Relatórios");
                    Console.WriteLine("0 - Sair");
                }
                else
                {
                    Console.WriteLine("1 - Listar produtos");
                    Console.WriteLine("2 - Vender produto");
                    Console.WriteLine("3 - Pedir reposição");
                    Console.WriteLine("0 - Sair");
                }

                Console.Write("Opção: ");
                string op = Console.ReadLine();

                if (op == "0") break;

                try
                {
                    // ================= ADMIN =================

                    if (user.Perfil == "ADMIN" && op == "1")
                    {
                        Console.Write("Nome: ");
                        string nome = Console.ReadLine();

                        Console.Write("Preço: ");
                        decimal preco = decimal.Parse(Console.ReadLine());

                        Console.Write("Quantidade: ");
                        int qtd = int.Parse(Console.ReadLine());

                        Console.Write("Estoque mínimo alerta: ");
                        int min = int.Parse(Console.ReadLine());

                        produtoService.CriarProduto(nome, preco, qtd, min, user.Perfil);
                        Console.WriteLine("Produto cadastrado.");
                    }

                    else if (user.Perfil == "ADMIN" && op == "2")
                    {
                        var lista = produtoService.Listar(user.Perfil);

                        foreach (var p in lista)
                        {
                            Console.Write($"{p.Id} - {p.Nome} | R$ {p.Preco} | Qtd:{p.Quantidade}");

                            if (p.Quantidade <= p.EstoqueMin)
                                Console.Write("  ⚠ BAIXO");

                            Console.WriteLine($" | Ativo:{p.Ativo}");
                        }
                    }

                    else if (user.Perfil == "ADMIN" && op == "3")
                    {
                        Console.Write("ID: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("Novo preço: ");
                        decimal preco = decimal.Parse(Console.ReadLine());

                        produtoService.AlterarPreco(id, preco, user.Perfil);
                        Console.WriteLine("Preço alterado.");
                    }

                    else if (user.Perfil == "ADMIN" && op == "4")
                    {
                        Console.Write("ID: ");
                        int id = int.Parse(Console.ReadLine());

                        produtoService.Inativar(id, user.Perfil);
                        Console.WriteLine("Produto inativado.");
                    }

                    else if (user.Perfil == "ADMIN" && op == "5")
                    {
                        Console.Write("ID: ");
                        int id = int.Parse(Console.ReadLine());

                        produtoService.Ativar(id, user.Perfil);
                        Console.WriteLine("Produto ativado.");
                    }

                    else if (user.Perfil == "ADMIN" && op == "6")
                    {
                        Console.WriteLine("\n=== RELATÓRIOS ===");
                        Console.WriteLine("1 - Vendas por dia");
                        Console.WriteLine("2 - Top produtos");
                        Console.Write("Opção: ");
                        string r = Console.ReadLine();

                        if (r == "1")
                            RelatorioRepo.VendasPorDia();
                        else if (r == "2")
                            RelatorioRepo.TopProdutos();
                    }

                    // ================= ATENDENTE =================

                    else if (user.Perfil == "ATENDENTE" && op == "1")
                    {
                        var lista = produtoService.Listar(user.Perfil);

                        foreach (var p in lista)
                        {
                            Console.Write($"{p.Id} - {p.Nome} | R$ {p.Preco} | Qtd:{p.Quantidade}");

                            if (p.Quantidade <= p.EstoqueMin)
                                Console.Write("  ⚠ BAIXO");

                            Console.WriteLine();
                        }
                    }

                   else if (user.Perfil == "ATENDENTE" && op == "2")
                    {
                        var pedido = new Pedido();
                        pedido.UsuarioId = user.Id;

                        while (true)
                        {
                            Console.Write("ID produto: ");
                            int id = int.Parse(Console.ReadLine());

                            Console.Write("Quantidade: ");
                            int qtd = int.Parse(Console.ReadLine());

                            try
                            {
                                pedidoService.AdicionarItem(pedido, id, qtd);
                                Console.WriteLine("Item adicionado!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Erro: " + ex.Message);
                                continue;
                            }

                            Console.Write("Adicionar mais produtos? (s/n): ");
                            string cont = Console.ReadLine().ToLower();

                            if (cont != "s")
                                break;
                        }

    pedidoService.FecharPedido(pedido);

    Console.WriteLine($"TOTAL: R$ {pedido.Total}");

    Console.Write("Confirmar venda? (s/n): ");
    if (Console.ReadLine().ToLower() == "s")
    {
        pedidoService.Salvar(pedido);
        Console.WriteLine("Venda salva!");
    }
}

                    else if (user.Perfil == "ATENDENTE" && op == "3")
                    {
                        Console.Write("ID produto: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("Quantidade reposição: ");
                        int qtd = int.Parse(Console.ReadLine());

                        PedidoRepo.Criar(id, qtd);
                        Console.WriteLine("Pedido de reposição enviado.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
        }
    }
}
