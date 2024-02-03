namespace Tree_KeyPer.Services;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? EmailAddress { get; set; }
    public string? WwwAddress { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public Service? LoggedWith { get; set; }
    public string Type { get; set; }
    public string UserName { get; set; }

    public Service(int id, string name, string? emailAddress, string? wwwAddress, string? login, string? password,
        DateTime? expirationDate, Service? loggedWith, string type, string userName)
    {
        Id = id;
        Name = name;
        EmailAddress = emailAddress;
        WwwAddress = wwwAddress;
        Login = login;
        Password = password;
        ExpirationDate = expirationDate;
        LoggedWith = loggedWith;
        Type = type;
        UserName = userName;
    }
}