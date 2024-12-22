using ObserverFire.Entities;

namespace ObserverFire.Abstractions;

public interface IUserRepository
{
    Task<User> GetUserById(Guid userId);
    Task Create (User user);
    Task Delete (Guid userId);
    Task<User> GetUserByApiKey(string apiKey);
}