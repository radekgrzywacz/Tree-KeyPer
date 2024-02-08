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

}