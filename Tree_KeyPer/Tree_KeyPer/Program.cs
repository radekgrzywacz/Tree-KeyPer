using Tree_KeyPer.Services;
using Tree_KeyPer.Tree_Data_Structure;

class Program
{
    public static void Main(string[] args)
    {
        var mail = new Mail("rad", "radox", "chuj", null);

        var date = DateTime.Today;
        var host = new Host("strona", "hostowa", "haslo", date);


        var root = new TreeNode<IService>(mail);
        var child = new TreeNode<IService>(host);
        
        root.AddChild(child);

        Console.WriteLine(root.Children[0].Children.Count);
    }
}