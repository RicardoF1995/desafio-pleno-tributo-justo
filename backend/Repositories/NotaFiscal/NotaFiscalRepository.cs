using backend.Models;
using Microsoft.Data.Sqlite;

namespace backend.Repositories
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        private readonly string _connectionString;

        public NotaFiscalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> InserirNotaFiscalAsync(int empresaId, NotaFiscal nota)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
                INSERT INTO nota_fiscal
                           (numero_nota,
                            data_emissao,
                            empresa_id)
                    VALUES ($numero_nota,
                            $data_emissao,
                            $empresa_id);

                SELECT last_insert_rowid();
            ";

                command.Parameters.AddWithValue("$numero_nota", nota.NumeroNota);
                command.Parameters.AddWithValue("$data_emissao", nota.DataEmissao);
                command.Parameters.AddWithValue("$empresa_id", empresaId);

                var result = await command.ExecuteScalarAsync();

                await transaction.CommitAsync();

                return Convert.ToInt32(result);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<NotaFiscal>> BuscarNotasPorEmpresaAsync(int empresaId)
        {
            var notas = new List<NotaFiscal>();

            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            // Buscar notas da empresa
            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT id,
                       numero_nota,
                       data_emissao
                  FROM nota_fiscal
                 WHERE empresa_id = $empresa_id;
            ";
            command.Parameters.AddWithValue("$empresa_id", empresaId);

            using var readerNotas = await command.ExecuteReaderAsync();

            while (await readerNotas.ReadAsync())
            {
                var nota = new NotaFiscal
                {
                    Id = readerNotas.GetInt32(0),
                    NumeroNota = readerNotas.GetString(1),
                    DataEmissao = readerNotas.GetDateTime(2),
                    LstItensNota = new List<ItemNota>()
                };

                notas.Add(nota);
            }

            return notas;
        }
    }
}
