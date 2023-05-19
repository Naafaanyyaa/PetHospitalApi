using Microsoft.Extensions.Configuration;
using PayPal.Api;
using PetHospital.Business.Exceptions;
using PetHospital.Business.Interfaces;
using PetHospital.Data.Enums;

namespace PetHospital.Business.Services
{
    public class PayPalService : IPayPalService
    {
        private Payment _createdPayment = null!;
        private readonly IConfiguration _configuration;
        private readonly string url;

        public PayPalService(IConfiguration configuration)
        {
            _configuration = configuration.GetSection("PayPal");
            url = Environment.GetEnvironmentVariable("ASPNETCORE_URLS")!.Split(";").First();
        }

        public async Task<Payment> CreatePayment(SubscriptionType type)
        {
            var apiContext = GetApiContext();

            try
            {
                Payment payment = new Payment()
                {
                    intent = "sale",
                    payer = new Payer() { payment_method = "paypal" },
                    transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            amount = new Amount()
                            {
                                currency = "EUR",
                                total = type switch
                                {
                                    SubscriptionType.Default => "100"
                                }
                                
                            },
                            description = "Test product"
                        }
                    },
                    redirect_urls = new RedirectUrls()
                    {
                        cancel_url = $"{url}/api/PayPal/CancelPayment",
                        return_url = "http://localhost:4200/profile"
                    }
                };

                _createdPayment = await Task.Run(() => payment.Create(apiContext));

                
            }
            catch (Exception ex)
            {
                throw new ValidationException("Cannot create payment");
            }
            return _createdPayment;
        }

        public async Task<Payment> ExecutePayment(string payerId, string paymentId)
        {
            var apiContext = GetApiContext();

            PaymentExecution paymentExecution = new PaymentExecution() { payer_id = payerId };

            Payment payment = new Payment() { id = paymentId };

            Payment executedPayment = await Task.Run(() => payment.Execute(apiContext, paymentExecution));

            return executedPayment;
        }

        private APIContext? GetApiContext()
        {
            var clientId = _configuration["Key"];

            var clientSecret = _configuration["Secret"];

            var accessToken = new OAuthTokenCredential(clientId, clientSecret).GetAccessToken();

            var apiContext = new APIContext(accessToken)
            {
                Config = ConfigManager.Instance.GetProperties()
            };

            return apiContext;
        }
    }
}