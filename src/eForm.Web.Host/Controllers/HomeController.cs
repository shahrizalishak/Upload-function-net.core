using Abp.Auditing;
using Microsoft.AspNetCore.Mvc;

namespace eForm.Web.Controllers
{
    public class HomeController : eFormControllerBase
    {
        [DisableAuditing]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Ui");
        }
    }
}
