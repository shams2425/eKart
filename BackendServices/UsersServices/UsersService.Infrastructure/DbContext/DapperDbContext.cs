using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace UsersService.Infrastructure.DbContext;

public class DapperDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    private readonly IDbConnection _dbconnection;

    public DapperDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("postgresConnection");
        _dbconnection = new NpgsqlConnection(_connectionString);
    }

    public IDbConnection Connection
    {
        get
        {
            return _dbconnection;
        }

    }
}
