using MySql.Data.MySqlClient;

namespace FilaRapida.Data
{
    public static class RelatorioRepo
    {
        public static void VendasPorDia()
        {
            using var conn = Db.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(@"
            SELECT DATE(criado_em) dia, SUM(total) total_dia
            FROM pedidos
            GROUP BY DATE(criado_em)
            ORDER BY dia DESC;
            ", conn);

            using var reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== VENDAS POR DIA ===");

            while (reader.Read())
            {
                Console.WriteLine($"{reader["dia"]} - R$ {reader["total_dia"]}");
            }
        }

        public static void TopProdutos()
        {
            using var conn = Db.GetConnection();
            conn.Open();

            var cmd = new MySqlCommand(@"
            SELECT p.nome, SUM(i.qtd) qtd_total
            FROM pedido_itens i
            JOIN produtos p ON p.id = i.produto_id
            GROUP BY p.nome
            ORDER BY qtd_total DESC;
            ", conn);

            using var reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== TOP PRODUTOS ===");

            while (reader.Read())
            {
                Console.WriteLine($"{reader["nome"]} - {reader["qtd_total"]} vendidos");
            }
        }

        public static void PedidosPorAtendente()
        {
            using var conn = Db.GetConnection();
            conn.Open();

            string sql = @"
            SELECT u.nome, COUNT(p.id) total
            FROM pedidos p
            JOIN usuarios u ON u.id = p.usuario_id
            GROUP BY u.nome
            ORDER BY total DESC";

            using var cmd = new MySqlCommand(sql, conn);
            using var r = cmd.ExecuteReader();

            Console.WriteLine("\n=== PEDIDOS POR ATENDENTE ===");

            while (r.Read())
            {
                Console.WriteLine($"{r.GetString("nome")} - {r.GetInt32("total")} pedidos");
            }
        }

    }
}
