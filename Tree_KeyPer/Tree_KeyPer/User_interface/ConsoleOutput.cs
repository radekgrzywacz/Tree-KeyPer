using PK3_project;

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
                // var logger = new Logger();
                // logger.Log(ex.Message);
                isValidLogin = false;
            }
        } while (!isValidLogin);

        return await sql.SearchForUserAsync(login);
    }
    
    
    
}