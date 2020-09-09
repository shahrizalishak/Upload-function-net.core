using System.Threading.Tasks;
using Abp.Application.Services;
using eForm.MultiTenancy.Payments.Dto;
using eForm.MultiTenancy.Payments.Stripe.Dto;

namespace eForm.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}