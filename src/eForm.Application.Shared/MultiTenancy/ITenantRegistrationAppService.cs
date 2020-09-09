using System.Threading.Tasks;
using Abp.Application.Services;
using eForm.Editions.Dto;
using eForm.MultiTenancy.Dto;

namespace eForm.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}