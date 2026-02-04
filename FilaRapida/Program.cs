using FilaRapida.Services;

namespace FilaRapida
{
    class Program
    {
        static void Main()
    {
        var auth = new AuthService();

        Console.WriteLine("1 - Login");
        Console.WriteLine("2 - Cadastrar usuário");
        Console.Write("Opção: ");
        string opcao = Console.ReadLine();

        if (opcao == "2")
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("Login: ");
            string login = Console.ReadLine();

            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            Console.Write("Perfil (ADMIN/ATENDENTE): ");
            string perfil = Console.ReadLine();

            auth.CadastrarUsuario(nome, login, senha, perfil);
            Console.WriteLine("Usuário cadastrado com sucesso.");
            return;
        }

        Console.Write("Login: ");
        string l = Console.ReadLine();

        Console.Write("Senha: ");
        string s = Console.ReadLine();

        var user = auth.Login(l, s);

        if (user == null)
        {
            Console.WriteLine("Login inválido.");
            return;
        }

        Console.WriteLine($"Bem-vindo {user.Nome} ({user.Perfil})");
    }
    }
}