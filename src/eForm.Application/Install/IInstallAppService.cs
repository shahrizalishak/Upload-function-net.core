using System.Threading.Tasks;
using Abp.Application.Services;
using eForm.Install.Dto;

namespace eForm.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}