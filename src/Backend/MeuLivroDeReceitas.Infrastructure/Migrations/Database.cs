using Dapper;
using MySqlConnector;
namespace MeuLivroDeReceitas.Infrastructure.Migrations;

public static class Database
{
    public static void CriarDatabase(string connectionString, string databaseName)
    {
        using var minhaConexao = new MySqlConnection(connectionString);

        var parametros = new DynamicParameters();
        parametros.Add("nome", databaseName);

        var registros = minhaConexao.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @nome", parametros);

        if (!registros.Any())
        {
            minhaConexao.Execute($"CREATE DATABASE {databaseName}");
        }

    }

   
}
