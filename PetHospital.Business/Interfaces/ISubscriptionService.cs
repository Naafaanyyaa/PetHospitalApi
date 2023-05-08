using PetHospital.Data.Entities;
using PetHospital.Data.Enums;

namespace PetHospital.Business.Interfaces
{
    public interface ISubscriptionService
    {
        Task<Subscription> AddSubscriptionAsync(string userId, SubscriptionType subscriptionType);
    }
}
