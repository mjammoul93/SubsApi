using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using SubsApi.DTOs;
using SubsApi.Helpers;
using SubsApi.Interfaces;
using SubsApi.Models;

namespace SubsApi.Data
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;
        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IList<SubscriptionDetailsDTO>> GetActiveSubsciptionsAsync()
        {
            var retryPolicy = RetryPolicyProvider.GetAsyncRetryPolicy();

            return await  retryPolicy.ExecuteAsync(async () =>
            {

                try
                {

                    var result = await _context.SubscriptionsDetails.FromSqlRaw("SELECT * FROM GetActiveSubscriptions()").ToListAsync();

                    return result;
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            });
        }

        public async Task<IList<SubscriptionDetailsDTO>> GetSubscriptionByUserIdAsync(int userId)
        {
            var retryPolicy = RetryPolicyProvider.GetAsyncRetryPolicy();
            return await retryPolicy.ExecuteAsync(async () =>
            {

                try
                {
                    string query = string.Format("SELECT * FROM GetSubscriptionDetailsByUserId({0})", userId);
                    var result = await _context.SubscriptionsDetails.FromSqlRaw(query).ToListAsync();
                    return result;

                 }
                catch (Exception ex)
                {
                throw new Exception(ex.Message, ex);
                }
            });
        }

        public async Task<GymSubscriptionType> GetSubscriptionTypeByIdAsync(int SubscribtionTypeId)
        {
            var retryPolicy = RetryPolicyProvider.GetAsyncRetryPolicy();
            return await retryPolicy.ExecuteAsync(async () =>
            {
                try
                {
                    return await _context.SubscriptionTypes.FindAsync(SubscribtionTypeId);
                }
       
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            });
        }

        public async Task<bool> SaveSubsciptionAsync(MembersSubsciptions membersSubsciptions)
        {
            var retryPolicy = RetryPolicyProvider.GetAsyncRetryPolicy();
            return await retryPolicy.ExecuteAsync(async () =>
            {
                try
                {
                    _context.Subsciptions.Add(membersSubsciptions);
                    return await _context.SaveChangesAsync() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            });
        }
    }
}
