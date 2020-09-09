using Abp.Application.Services;
using Abp.Application.Services.Dto;
using eForm.Authorization.Permissions.Dto;

namespace eForm.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
