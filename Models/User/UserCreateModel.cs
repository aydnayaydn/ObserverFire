namespace ObserverFire.Models;

public class UserCreateModel
{
    public required string UserName { get; set; }
    public required string Role { get; set; } // admin, stackholder
}