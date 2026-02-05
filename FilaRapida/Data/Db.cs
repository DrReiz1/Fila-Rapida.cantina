using MySql.Data.MySqlClient;

namespace FilaRapida.Data
{
    public static class Db
    {
        public static string ConnStr =
            "server=localhost;user=root;password=;database=fila_rapida";

             public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnStr);
        }
    }
}

