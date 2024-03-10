using SubsApi.Models;

namespace SubsApi.Interfaces
{
    public interface IUserRepository
    {
        void update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetAppUsersByIdAsync(int id);
        Task<AppUser> GetUserByUsernamesync(string username);
    }
}
