using MySql.Data.MySqlClient;
using FilaRapida.Models;

namespace FilaRapida.Data
{
    public static class PedidoRepo
    {
        public static void SalvarPedido(Pedido pedido)
        {
            using var conn = Db.GetConnection();
            conn.Open();

            using var trans = conn.BeginTransaction();

            try
            {
                var cmdPedido = new MySqlCommand(@"
                INSERT INTO pedidos (usuario_id, total)
                VALUES (@usuario, @total);
                SELECT LAST_INSERT_ID();
                ", conn, trans);

                cmdPedido.Parameters.AddWithValue("@usuario", pedido.UsuarioId);
                cmdPedido.Parameters.AddWithValue("@total", pedido.Total);

                int pedidoId = Convert.ToInt32(cmdPedido.ExecuteScalar());

                foreach (var item in pedido.Itens)
                {
                    var cmdItem = new MySqlCommand(@"
                    INSERT INTO pedido_itens 
                    (pedido_id, produto_id, qtd, preco_unit)
                    VALUES (@pedido, @produto, @qtd, @preco)
                    ", conn, trans);

                    cmdItem.Parameters.AddWithValue("@pedido", pedidoId);
                    cmdItem.Parameters.AddWithValue("@produto", item.ProdutoId);
                    cmdItem.Parameters.AddWithValue("@qtd", item.Qtd);
                    cmdItem.Parameters.AddWithValue("@preco", item.PrecoUnit);

                    cmdItem.ExecuteNonQuery();

                    var cmdEstoque = new MySqlCommand(@"
                    UPDATE produtos 
                    SET quantidade = quantidade - @qtd
                    WHERE id = @id
                    ", conn, trans);

                    cmdEstoque.Parameters.AddWithValue("@qtd", item.Qtd);
                    cmdEstoque.Parameters.AddWithValue("@id", item.ProdutoId);
                    cmdEstoque.ExecuteNonQuery();
                }

                trans.Commit();
                Console.WriteLine("Pedido salvo!");
            }
            catch
            {
                trans.Rollback();
                Console.WriteLine("Erro. Pedido cancelado.");
            }
        }
        public static void Criar(int produtoId, int qtd)
        {
            using var conn = Db.GetConnection();
            conn.Open();

            string sql = @"INSERT INTO pedidos_reposicao 
                        (produto_id, quantidade) 
                        VALUES (@p,@q)";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@p", produtoId);
            cmd.Parameters.AddWithValue("@q", qtd);
            cmd.ExecuteNonQuery();
        }

    }
}
