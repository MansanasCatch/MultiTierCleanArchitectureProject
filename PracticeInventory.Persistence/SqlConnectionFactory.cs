using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PracticeInventory.Domain.Interfaces;

namespace PracticeInventory.Persistence;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;
    public SqlConnectionFactory(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public SqlConnection Create()
    {
        return new SqlConnection(_connectionString);
    }
}