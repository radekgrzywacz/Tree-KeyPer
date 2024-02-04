namespace Tree_KeyPer;

public class ConsoleOutput
{
    private SqlDataAccess sql = new SqlDataAccess();
    public async Task StartProgram()
    {
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
                await logIn();
                break;
            default:
                Console.WriteLine("df");
                break;
        }


    }

    public async Task logIn()
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
                Console.WriteLine($"Wrong login AGAIN!!! Exception: {ex.Message}");
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

    }
}