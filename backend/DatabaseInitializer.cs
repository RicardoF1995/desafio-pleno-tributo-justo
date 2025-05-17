using Microsoft.Data.Sqlite;

public static class DatabaseInitializer
{
    public static void Initialize()
    {
        var connectionString = "Data Source=desafioTributoJusto.db";

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE empresa (
                         id INTEGER PRIMARY KEY AUTOINCREMENT,
                         cnpj TEXT NOT NULL UNIQUE,
                         razao_social TEXT NOT NULL
            );

            CREATE TABLE item_nota (
                         id INTEGER PRIMARY KEY AUTOINCREMENT,
                         nota_id INTEGER NOT NULL,
                         codigo_item TEXT NOT NULL,
                         descricao_item TEXT NOT NULL,
                         quantidade REAL NOT NULL,
                         valor_unitario REAL NOT NULL,
                         imposto_item REAL NOT NULL,
             FOREIGN KEY (nota_id) REFERENCES nota_fiscal(id)
            );

            CREATE TABLE nota_fiscal (
                         id INTEGER PRIMARY KEY AUTOINCREMENT,
                         numero_nota TEXT NOT NULL,
                         data_emissao TEXT NOT NULL,
                         empresa_id INTEGER NOT NULL,
            FOREIGN KEY (empresa_id) REFERENCES empresa(id)
            );

            CREATE TABLE usuario (
                         id INTEGER PRIMARY KEY AUTOINCREMENT,
                         nome_usuario TEXT NOT NULL UNIQUE,
                         senha_hash TEXT NOT NULL
            );
        ";

        command.ExecuteNonQuery();
    }
}
