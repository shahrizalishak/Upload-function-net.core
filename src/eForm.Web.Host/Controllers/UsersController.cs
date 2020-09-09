using Abp.AspNetCore.Mvc.Authorization;
using eForm.Authorization;
using eForm.Storage;
using Abp.BackgroundJobs;

namespace eForm.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}