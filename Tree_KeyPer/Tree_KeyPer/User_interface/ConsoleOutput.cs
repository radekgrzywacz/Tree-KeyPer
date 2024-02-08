using System.Security.Claims;
using Tree_KeyPer.Services;
using Tree_KeyPer.Tree_Data_Structure;

namespace Tree_KeyPer;

public class ConsoleOutput
{
    private SqlDataAccess sql = new SqlDataAccess();
    public async Task StartProgram()
    {
        // Choosing user
        User user = null;
        Console.WriteLine("Hi, welcome to Tree-KeyPer. Please log-in or create new user account.");
        Console.WriteLine("Press 1 if you want to log into existing user or 2 if you want to create new user.");
        string userInput;
        do
        {
            Console.WriteLine("Insert a number:");
            userInput = Console.ReadLine();
        } while (userInput != "1" && userInput != "2");

        switch (userInput)
        {
            case "1":
                user = await LogIn();
                break;
            case "2":
                user = await CreateAccount();
                break;
            default:
                Console.WriteLine("df");
                break;
        }

        var nodes = await sql.GetUsersServicesAsync(user.login);

        
        // Main part
        Console.WriteLine("What do you want to do? \n 1. Check your services, 2. Add new service");
        do
        {
            Console.WriteLine("Insert a number:");
            userInput = Console.ReadLine();
        } while (userInput != "1" && userInput != "2");
        
        switch (userInput)
        {
            case "1":
                
                break;
            case "2":
                
                break;
            default:
                Console.WriteLine("df");
                break;
        }


    }

    public async Task<User> LogIn()
    {
        User user = null;
        bool isUserValid = true;
        Console.WriteLine("Glad to see you again. ");
        do
        {
            isUserValid = true;
            Console.WriteLine("Enter your login:");
            var login = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            var password = Console.ReadLine();
            try
            {
                user = await sql.SearchForUserAsync(login);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("There is no account that uses this login or password");
                isUserValid = false;
            }

            try
            {
                if (password != user.password || login != user.login)
                {
                    Console.WriteLine("There is no account that uses this login or password");
                    isUserValid = false;
                }
                else
                {
                    Console.WriteLine("Welcome");
                }
            }
            catch (NullReferenceException)
            {
                isUserValid = false;
            }
        } while (!isUserValid);

        return user;

    }

    public async Task<User> CreateAccount()
    {
        bool isValidLogin = true;
        Console.WriteLine("We're happy that you want to join us!");

        string login;
        do
        {
            isValidLogin = true;
            Console.WriteLine("Insert your login:");
            login = Console.ReadLine();

            Console.WriteLine("Insert your password");
            var password = Console.ReadLine();

            try
            {
                await sql.CreateUser(login, password);
            }
            catch (Npgsql.PostgresException ex)
            {
                isValidLogin = false;
            }
        } while (!isValidLogin);

        return await sql.SearchForUserAsync(login);
    }

    public void CheckServices(List<TreeNode<Service>> nodes)
    {
        Console.WriteLine("Do you want to check a specific one (1), list your all services (2) or delete a service (3)?");
        string userInput;
        do
        {
            Console.WriteLine("Insert a number:");
            userInput = Console.ReadLine();
        } while (userInput != "1" && userInput != "2" && userInput != "3");

        switch (userInput)
        {
            case "1":
                
                break;
            case "2":

                break;
            case "3":

                break;
            default:
                Console.WriteLine("df");
                break;
        }
    }

    public void CheckSpecificService(List<TreeNode<Service>> nodes)
    {
        bool isInputValid = true;
        do
        {
            Console.WriteLine(
                "Would you like to see all your services first? Insert 'y' as yes or write your service name:");
            var userInput = Console.ReadLine();

            if (userInput == "y")
            {
                ListServices(nodes);
                Console.WriteLine("\nWhat service would you like to see?");
                userInput = Console.ReadLine();
            }else if (userInput == "n")
            {
                break;
            }
            
            var service = nodes.FirstOrDefault(n => n.Data.Name == userInput);
            if (service == null)
            {
                Console.WriteLine(
                    "We couldn't find service with name like this. Would you like to try again? If no, please insert 'n'");
                isInputValid = false;
                continue;
            }

            Console.WriteLine($"----{service.Data.Name}-----");
            Console.WriteLine($"-> Login: {service.Data.Login}");
            Console.WriteLine($"-> Password: {service.Data.Password}");
            Console.WriteLine($"-> Email address: {service.Data.Email_Address}");
            Console.WriteLine($"-> WWW address: {service.Data.Www_Address}");
            Console.WriteLine($"-> Expires: {service.Data.Expiration_Date}");
            try
            {
                Console.WriteLine(
                    $"-> Logged with service: {nodes.FirstOrDefault(n => n.Data.Id == service.Data.Logged_With_id).Data.Name}");
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(
                    $"-> Logged with service:");
                var log = new Logger();
                log.Log(ex.Message);
            }

            Console.WriteLine($"-> Type: {service.Data.Type}");
            var childrenNames = service.Children.Select(c => c.Data.Name);
            Console.WriteLine($"-> Used by: {string.Join(", ", childrenNames)}");
            var parentsNames = service.Parents.Select(p => p.Data.Name);
            Console.WriteLine($"-> Used by: {string.Join(", ", parentsNames)}");
            

            Console.WriteLine("Would you like to check something else? (y/n)");
            do
            {
                userInput = Console.ReadLine();
            } while (userInput != "y" && userInput != "n");

            if (userInput == "y") isInputValid = false;



        } while (!isInputValid);
    }

    public void ListServices(List<TreeNode<Service>> nodes)
    {
        var last = nodes.Last();
        foreach (var node in nodes)
        {
            Console.Write(node.Equals(last) ? $"{node.Data.Name}" : $"{node.Data.Name}, ");
        }
    }

    public void SearchTree(List<TreeNode<Service>> nodes)
    {
        // for (int i = 0; i < nodes.Count; i++)
        // {
        //     
        // }

        foreach (var node in nodes)
        {
            SearchTree(node.Parents.ToList());
            Console.WriteLine(node.Data.Name);
        }
    }


}