using Tree_KeyPer;

class Program
{
    
    public static async Task Main(string[] args)
    {
        var console = new ConsoleOutput();
        await console.StartProgram();
        //var sql = new SqlDataAccess();
        //var nodes = await sql.GetUsersServicesAsync("Radek");
        //console.CheckSpecificService(nodes);
        Console.WriteLine("Koniec");

    }
}