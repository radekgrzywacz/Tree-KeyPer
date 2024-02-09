using System.Collections;
using Dapper;
using Npgsql;
using Tree_KeyPer.Services;
using Tree_KeyPer.Tree_Data_Structure;

namespace Tree_KeyPer;

public class SqlDataAccess
{
    private string _connectionString =
        @"Server=localhost;Port=5432;Username=radek;Password=kakadu77;Database=tree_keyper";

    public async Task<List<TreeNode<Service>>> GetUsersServicesAsync(string userName)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
        {
            var sql = $"SELECT * FROM node WHERE user_name = '{userName}'";

            List<TreeNode<Service>> nodes = new List<TreeNode<Service>>();
            
            var servicesList = await conn.QueryAsync<Service>(sql);
            foreach (var service in servicesList)
            {
                nodes.Add(new TreeNode<Service>(service));    
            }

            await GetNodesRelationsAsync(nodes);
            return nodes;

        }
    }
    public async Task GetNodesRelationsAsync(List<TreeNode<Service>?> nodes)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
        {
            // Set parents if account is used to log in
            foreach (var node in nodes)
            {
                if (node.Data.Logged_With_id != null)
                {
                    node.Parents.Add(nodes.FirstOrDefault(n => n.Data.Id == node.Data.Logged_With_id));
                    nodes.FirstOrDefault(n => n.Data.Id == node.Data.Logged_With_id).Children.Add(node);
                }
            }

            foreach (var node in nodes)
            {
                var childrenIds = await conn.QueryAsync<int>(
                    $"SELECT child_id FROM node_relations WHERE parent_id = {node.Data.Id}");

                var children = nodes.Where(n => childrenIds.Contains(n.Data.Id)).ToList();
                foreach (var child in children)
                {
                    node.Children.Add(child);
                    child.Parents.Add(node);
                }
            }

            Console.WriteLine("sda");
        }   
    }

    public async Task<User> SearchForUserAsync(string login)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
        {
            var sql = $"SELECT login, password FROM users WHERE login = '{login}'";

            var user = await conn.QuerySingleAsync<User>(sql);
            return user;
            
        }
    }

    public async Task CreateUser(string login, string password)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
        {
            var sql = $"INSERT INTO users (login, password) VALUES ('{login}', '{password}');";

            await conn.ExecuteAsync(sql);

            Console.WriteLine("Account added.");
            
        }
    }

    public async Task<List<string>> GetTypesAsync()
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
        {
            var sql = "SELECT name FROM groups;";

            var types = await conn.QueryAsync<string>(sql);

            return types.ToList();
        }
    }

    public async Task AddUserAsync(string name, string? emailAddres, string? wwwAddress, string? login, string? password, DateTime? expirationDate, int? loggedWithId, string type, string userName)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
        {
            var sql =
                $"INSERT INTO node (name, email_address, www_address, login, password, expiration_date, logged_with, type, user_name) " +
                $"VALUES ('{name}', '{emailAddres}', '{wwwAddress}', '{login}', '{password}', '{expirationDate}',{loggedWithId}, '{type}', '{userName}')";
            
            await conn.ExecuteAsync(sql);
            
            Console.WriteLine("Service added.");
        }
    }

    public async Task<int> SearchForService(string serviceName, string userName)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
        {
            var sql = $"SELECT id FROM node WHERE name = '{serviceName}', user_name = '{userName}';";

            var id = await conn.QuerySingleAsync<int>(sql);

            return id;
        }
    }

    public async Task CreateRelation(int parentId, int childId)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
        {
            var sql = $"INSERT INTO node_relations (parent_id, child_id) VALUES ({parentId},{childId});";

            await conn.ExecuteAsync(sql);

            Console.WriteLine("Relation created.");
        }
    }
}