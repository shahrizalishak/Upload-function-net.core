using System.Threading.Tasks;
using eForm.Authorization.Users;

namespace eForm.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
