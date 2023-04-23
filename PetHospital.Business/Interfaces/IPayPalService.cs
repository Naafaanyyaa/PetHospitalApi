using PayPal.Api;
using PetHospital.Data.Enums;

namespace PetHospital.Business.Interfaces
{
    public interface IPayPalService
    {
        Task<Payment> CreatePayment(SubscriptionType type);

        Task<Payment> ExecutePayment(string payerId, string paymentId);
    }
}