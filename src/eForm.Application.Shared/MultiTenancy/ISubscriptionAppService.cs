using System.Threading.Tasks;
using Abp.Application.Services;

namespace eForm.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
