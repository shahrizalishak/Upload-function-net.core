using System.Threading.Tasks;
using Abp.Application.Services;
using eForm.Configuration.Host.Dto;

namespace eForm.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
