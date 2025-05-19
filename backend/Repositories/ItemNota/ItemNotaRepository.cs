using backend.Models;
using Microsoft.Data.Sqlite;

namespace backend.Repositories
{
    public class ItemNotaRepository : IItemNotaRepository
    {
        private readonly string _connectionString;

        public ItemNotaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task InserirItemNotaAsync(ItemNota itemNota, int notaFiscalId)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO item_nota
                            (codigo_item,
                            descricao_item,
                            quantidade,
                            valor_unitario,
                            imposto_item,
                            nota_id)
                    VALUES ($codigo_item,
                            $descricao_item,
                            $quantidade,
                            $valor_unitario,
                            $imposto_item,
                            $nota_id);
            ";

            command.Parameters.AddWithValue("$codigo_item", itemNota.CodigoItem);
            command.Parameters.AddWithValue("$descricao_item", itemNota.DescricaoItem);
            command.Parameters.AddWithValue("$quantidade", itemNota.Quantidade);
            command.Parameters.AddWithValue("$valor_unitario", itemNota.ValorUnitario);
            command.Parameters.AddWithValue("$imposto_item", itemNota.ImpostoItem);
            command.Parameters.AddWithValue("$nota_id", notaFiscalId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<List<ItemNota>> BuscarItensPorNotaFiscalAsync(int notaFiscalId)
        {
            var itens = new List<ItemNota>();

            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT id,
                       codigo_item,
                       descricao_item,
                       quantidade,
                       valor_unitario,
                       imposto_item
                  FROM item_nota
                 WHERE nota_id = $nota_id;
            ";
            command.Parameters.AddWithValue("$nota_id", notaFiscalId);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var item = new ItemNota
                {
                    Id = reader.GetInt32(0),
                    CodigoItem = reader.GetString(1),
                    DescricaoItem = reader.GetString(2),
                    Quantidade = reader.GetDouble(3),
                    ValorUnitario = reader.GetDouble(4),
                    ImpostoItem = reader.GetDouble(5)
                };

                itens.Add(item);
            }

            return itens;
        }
    }
}