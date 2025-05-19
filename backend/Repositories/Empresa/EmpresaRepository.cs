using backend.Models;
using Microsoft.Data.Sqlite;

namespace backend.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly string _connectionString;

        public EmpresaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Empresa>> BuscarTodasEmpresasAsync()
        {
            var empresas = new List<Empresa>();

            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT id,
                       cnpj,
                       razao_social
                  FROM empresa
            ";

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var empresa = new Empresa
                {
                    Id = reader.GetInt32(0),
                    Cnpj = reader.GetString(1),
                    RazaoSocial = reader.GetString(2)
                };

                empresas.Add(empresa);
            }

            return empresas;
        }

        public async Task<int> InserirEmpresaAsync(Empresa empresa)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                var command = connection.CreateCommand();
                command.CommandText = @"
                INSERT INTO empresa
                           (cnpj,
                            razao_social)
                    VALUES ($cnpj,
                            $razao_social);
                            
                SELECT last_insert_rowid();
            ";
                command.Parameters.AddWithValue("$cnpj", empresa.Cnpj);
                command.Parameters.AddWithValue("$razao_social", empresa.RazaoSocial);

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

        public async Task<Empresa?> BuscarEmpresaPorCnpjAsync(string cnpj)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT id,
                       cnpj,
                       razao_social
                  FROM empresa
                 WHERE cnpj = $cnpj;
            ";
            command.Parameters.AddWithValue("$cnpj", cnpj);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Empresa
                {
                    Id = reader.GetInt32(0),
                    Cnpj = reader.GetString(1),
                    RazaoSocial = reader.GetString(2),
                    LstNotasFiscais = new List<NotaFiscal>() // Se quiser buscar notas depois
                };
            }

            return null;
        }

        public async Task<bool> VerificarEmpresaExistentePorCnpjAsync(string cnpj)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT id
                  FROM empresa
                 WHERE cnpj = $cnpj;
            ";
            command.Parameters.AddWithValue("$cnpj", cnpj);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result) > 0;
        }
    }
}
