namespace Tree_KeyPer.Services;

public class Service
{
    public string Name { get; }
    public string AccountName { get; }
    public string Password { get; }
    public DateTime? ExpirationDate { get; }

    public Service(string name, string accountName, string password, DateTime? expiration)
    {
        Name = name;
        AccountName = accountName;
        Password = password;
        ExpirationDate = expiration;
    }
}