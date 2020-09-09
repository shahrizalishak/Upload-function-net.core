using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using eForm.Authorization.Users.Dto;

namespace eForm.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<ListResultDto<UserLoginAttemptDto>> GetRecentUserLoginAttempts();
    }
}
