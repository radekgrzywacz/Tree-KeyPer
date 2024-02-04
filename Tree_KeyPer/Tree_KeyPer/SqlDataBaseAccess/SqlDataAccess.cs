using System.Collections;
using Dapper;
using Npgsql;
using Tree_KeyPer.Services;

namespace Tree_KeyPer;

public class SqlDataAccess
{
    private string _connectionString =
        @"Server=localhost;Port=5432;Username=radek;Password=kakadu77;Database=tree_keyper";

    public async Task<IEnumerable<Service>> GetUsersNodesAsync(string userName)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
        {
            var sql = $"SELECT * FROM node WHERE user_name = '{userName}'";

            IEnumerable<Service> services = null;
            try
            {
                services = await conn.QueryAsync<Service>(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return services;
        }
    }

    public async Task<User> SearchForUserAsync(string login)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
        {
            var sql = $"SELECT login, password FROM users WHERE login = '{login}'";

            var user = await conn.QuerySingleAsync<User>(sql);
            Console.WriteLine(user.login + " " + user.password);
            return user;
            
        }
    }

}