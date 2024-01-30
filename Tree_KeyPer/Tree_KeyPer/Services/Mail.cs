namespace Tree_KeyPer.Services;

public class Mail : IService
{
    public string Name { get; }
    public string AccountName { get; }
    public string Password { get; }
    public DateTime? ExpirationDate { get; }

    public Mail(string name, string accountName, string password, DateTime? expiration)
    {
        Name = name;
        AccountName = accountName;
        Password = password;
        ExpirationDate = expiration;
    }
    
}