using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SubsApi.Helpers;
using SubsApi.Interfaces;
using SubsApi.Models;

namespace SubsApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AppUser> GetAppUsersByIdAsync(int id)
        {
            var retryPolicy = RetryPolicyProvider.GetAsyncRetryPolicy();
            return await retryPolicy.ExecuteAsync(async () =>
            {
                 try
                {
                    return await _context.Users.FindAsync(id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            });
        }

        public async Task<AppUser> GetUserByUsernamesync(string username)
        {
            try
            {
                return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void update(AppUser user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
