using Tree_KeyPer;

class Program
{
    
    public static async Task Main(string[] args)
    {
        var console = new ConsoleOutput();
        await console.StartProgram();
        var sql = new SqlDataAccess();
        
    }
}