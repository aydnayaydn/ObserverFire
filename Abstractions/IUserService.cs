using ObserverFire.Models;

namespace ObserverFire.Abstractions;

public interface IUserService
{
    Task<UserViewModel> GetUserById(Guid userId);
    Task<UserViewModel> GetUserByApiKey(string apiKey);
    Task Create (UserCreateModel user);
    Task Delete (Guid userId);
}