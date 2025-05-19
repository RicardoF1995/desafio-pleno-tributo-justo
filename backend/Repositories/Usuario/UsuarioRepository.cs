using backend.Models;
using Microsoft.Data.Sqlite;

namespace backend.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task CadastrarUsuarioAsync(Usuario usuario)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO usuario
                           (nome_usuario, 
                            senha_hash)
                    VALUES ($nome_usuario,
                            $senha_hash);
            ";
            command.Parameters.AddWithValue("$nome_usuario", usuario.NomeUsuario);
            command.Parameters.AddWithValue("$senha_hash", usuario.SenhaHash);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<bool> VerificarUsuarioExistentePorNomeAsync(string nomeUsuario)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"SELECT id
                                      FROM usuario 
                                     WHERE nome_usuario = $nome_usuario";
            command.Parameters.AddWithValue("$nome_usuario", nomeUsuario);

            var result = await command.ExecuteScalarAsync();

            return result != null;
        }

        public async Task<Usuario?> LoginUsuarioAsync(string nomeUsuario)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT id,
                       nome_usuario,
                       senha_hash
                  FROM usuario 
                 WHERE nome_usuario = $nome_usuario
            ";
            command.Parameters.AddWithValue("$nome_usuario", nomeUsuario);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Usuario
                {
                    Id = reader.GetInt32(0),
                    NomeUsuario = reader.GetString(1),
                    SenhaHash = reader.GetString(2)
                };
            }

            return null;
        }
    }
}
