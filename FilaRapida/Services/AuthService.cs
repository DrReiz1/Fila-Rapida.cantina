using FilaRapida.Data;
using FilaRapida.Models;
using FilaRapida.Security;

namespace FilaRapida.Services
{
    public class AuthService
    {
        public UserDto Login(string login, string senha)
        {
            string hash = Hash.Sha256(senha);
            return UserRepo.Login(login, hash);
        }

        public void CadastrarUsuario(
            string nome,
            string login,
            string senha,
            string perfil)
        {
            string hash = Hash.Sha256(senha);
            UserRepo.CriarUsuario(nome, login, hash, perfil);
        }

    }

    
}
