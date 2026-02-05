using MySql.Data.MySqlClient;
using FilaRapida.Models;
using System.Collections.Generic;

namespace FilaRapida.Data
{
    public static class ProdutoRepo
    {
        public static void Criar(string nome, decimal preco, int qtd, int min)
{
        using var conn = Db.GetConnection();
        conn.Open();

        string sql = @"INSERT INTO produtos 
        (nome, preco, quantidade, estoque_min) 
        VALUES (@n,@p,@q,@m)";

        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@n", nome);
        cmd.Parameters.AddWithValue("@p", preco);
        cmd.Parameters.AddWithValue("@q", qtd);
        cmd.Parameters.AddWithValue("@m", min);
        cmd.ExecuteNonQuery();
    }


        public static List<ProdutoDto> ListarTodos()
        {
            var lista = new List<ProdutoDto>();

            using var conn = Db.GetConnection();
            conn.Open();

            string sql = "SELECT * FROM produtos";
            using var cmd = new MySqlCommand(sql, conn);
            using var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
               lista.Add(new ProdutoDto
            {
                Id = rd.GetInt32("id"),
                Nome = rd.GetString("nome"),
                Preco = rd.GetDecimal("preco"),
                Ativo = rd.GetBoolean("ativo"),
                Quantidade = rd.GetInt32("quantidade"),
                EstoqueMin = rd.GetInt32("estoque_min")
            });

            }

            return lista;
        }

        public static List<ProdutoDto> ListarAtivos()
        {
            var lista = new List<ProdutoDto>();

            using var conn = Db.GetConnection();
            conn.Open();

            string sql = "SELECT * FROM produtos WHERE ativo=1";
            using var cmd = new MySqlCommand(sql, conn);
            using var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                lista.Add(new ProdutoDto
                {
                    Id = rd.GetInt32("id"),
                    Nome = rd.GetString("nome"),
                    Preco = rd.GetDecimal("preco"),
                    Quantidade = rd.GetInt32("quantidade"),
                    EstoqueMin = rd.GetInt32("estoque_min"),
                    Ativo = rd.GetBoolean("ativo")
                });
            }

            return lista;
        }

        public static void AlterarPreco(int id, decimal preco)
        {
            using var conn = Db.GetConnection();
            conn.Open();

            string sql = "UPDATE produtos SET preco=@p WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@p", preco);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public static void AlterarQuantidade(int id, int qtd)
        {
            using var conn = Db.GetConnection();
            conn.Open();

            string sql = "UPDATE produtos SET quantidade=@q WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@q", qtd);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public static void Inativar(int id)
        {
            using var conn = Db.GetConnection();
            conn.Open();

            string sql = "UPDATE produtos SET ativo=0 WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public static void Ativar(int id)
        {
            using var conn = Db.GetConnection();
            conn.Open();

            string sql = "UPDATE produtos SET ativo=1 WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public static ProdutoDto Buscar(int id)
        {
            using var conn = Db.GetConnection();
            conn.Open();

            string sql = "SELECT * FROM produtos WHERE id=@id";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                return new ProdutoDto
                {
                    Id = rd.GetInt32("id"),
                    Nome = rd.GetString("nome"),
                    Preco = rd.GetDecimal("preco"),
                    Ativo = rd.GetBoolean("ativo"),
                    Quantidade = rd.GetInt32("quantidade"),
                    EstoqueMin = rd.GetInt32("estoque_min")
                };
            }

            return null;
        }

    }
}
