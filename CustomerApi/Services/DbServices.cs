using CustomerApi.IServices;
using Dapper;
using Microsoft.Extensions.Configuration;  
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;               
using System.Threading.Tasks;

public class DbServices : IDbServices
{
    private readonly IConfiguration _config;

    public DbServices(IConfiguration config)
    {
        _config = config;
    }

    private IDbConnection CreateConnection()
    {
        var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        connection.Open();
        return connection;
    }



    public async Task<T> GetAsync<T>(string command, object parms)
    {
        using var connection = CreateConnection();
        var result = await connection.QueryFirstOrDefaultAsync<T>(
            command,
            parms,
            commandType: CommandType.StoredProcedure
        );
        return result;
    }

    public async Task<List<T>> GetAll<T>(string command, object parms)
    {
        using var connection = CreateConnection();
        var result = await connection.QueryAsync<T>(
            command,
            parms,
            commandType: CommandType.StoredProcedure
        );
        return result.AsList();
    }

    public async Task<int> EditData(string command, object parms)
    {
        using var connection = CreateConnection();
        var affectedRows = await connection.ExecuteAsync(
            command,
            parms,
            commandType: CommandType.StoredProcedure
        );
        return affectedRows;
    }
}
