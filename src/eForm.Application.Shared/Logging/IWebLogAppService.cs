using Abp.Application.Services;
using eForm.Dto;
using eForm.Logging.Dto;

namespace eForm.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
