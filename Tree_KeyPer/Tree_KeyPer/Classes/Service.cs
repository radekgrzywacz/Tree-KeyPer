namespace Tree_KeyPer.Services;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Email_Address { get; set; }
    public string? Www_Address { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public DateTime? Expiration_Date { get; set; }
    public Service? Logged_With { get; set; }
    public string Type { get; set; }
    public string User_Name { get; set; }

    // public Service(int id, string name, string? emailAddress, string? wwwAddress, string? login, string? password,
    //     DateTime? expirationDate, Service? loggedWith, string type, string userName)
    // {
    //     Id = id;
    //     Name = name;
    //     EmailAddress = emailAddress;
    //     WwwAddress = wwwAddress;
    //     Login = login;
    //     Password = password;
    //     ExpirationDate = expirationDate;
    //     LoggedWith = loggedWith;
    //     Type = type;
    //     UserName = userName;
    // }
}