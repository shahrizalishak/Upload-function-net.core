using System.Threading.Tasks;
using Abp.Webhooks;

namespace eForm.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
