using System.Threading.Tasks;
using Abp.Application.Services;
using eForm.Configuration.Tenants.Dto;

namespace eForm.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
