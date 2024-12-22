namespace ObserverFire.Models;

public class UserViewModel
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Role { get; set; } // admin, stackholder

    public string ApiKey { get; set; }
}