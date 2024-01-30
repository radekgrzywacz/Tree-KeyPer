namespace Tree_KeyPer.Services;

public interface IService
{
    string Name { get; }
    string AccountName { get; }
    string Password { get; }
    DateTime? ExpirationDate { get; }
}