using RaftLabs.ReqResApiClient.Models;

namespace RaftLabs.ReqResApiClient.Services.Abstraction
{
    public interface IExternalUserService
    {
        Task<User?> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}
