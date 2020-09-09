using System.Threading.Tasks;
using Abp.Application.Services;
using eForm.Sessions.Dto;

namespace eForm.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
