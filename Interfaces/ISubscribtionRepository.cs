using SubsApi.DTOs;
using SubsApi.Models;

namespace SubsApi.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<IList<SubscriptionDetailsDTO>> GetActiveSubsciptionsAsync();
        Task<IList<SubscriptionDetailsDTO>> GetSubscriptionByUserIdAsync(int userId);
        Task<GymSubscriptionType> GetSubscriptionTypeByIdAsync(int SubscribtionTypeId);
        Task<bool> SaveSubsciptionAsync(MembersSubsciptions membersSubsciptions);
    }
}
