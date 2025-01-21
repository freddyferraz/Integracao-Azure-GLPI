using Integracao.Infra.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Integracao.Infra;
public class DbSession : IDbSession
{
    private const string SqlConnectionString = "connectionStrings";
    private readonly IDbConnection _connection;

    public DbSession(IConfiguration configuration)
    {
        _connection = new SqlConnection(configuration[SqlConnectionString]);

        _connection.Open();
    }

    public IDbConnection Connection => _connection;

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
    }
}