using MySql.Data.MySqlClient;
using FilaRapida.Models;

namespace FilaRapida.Data
{
    public static class UserRepo
    {
        public static UserDto Login(string login, string hash)
        {
            using var conn = new MySqlConnection(Db.ConnStr);
            conn.Open();

            string sql = @"
                SELECT id, nome, perfil
                FROM usuarios
                WHERE login = @login
                  AND senha_hash = @hash
                  AND ativo = 1";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@hash", hash);

            using var reader = cmd.ExecuteReader();

            if (!reader.Read())
                return null;

            return new UserDto
            {
                Id = reader.GetInt32("id"),
                Nome = reader.GetString("nome"),
                Perfil = reader.GetString("perfil")
            };

            
        }
        public static void CriarUsuario(
                string nome,
                string login,
                string hash,
                string perfil)
            {
                using var conn = new MySqlConnection(Db.ConnStr);
                conn.Open();

                string sql = @"
                    INSERT INTO usuarios (nome, login, senha_hash, perfil)
                    VALUES (@nome, @login, @hash, @perfil)";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@hash", hash);
                cmd.Parameters.AddWithValue("@perfil", perfil);

                cmd.ExecuteNonQuery();
            }

    }

    
}
