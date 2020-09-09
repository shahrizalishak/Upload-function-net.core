using Microsoft.AspNetCore.Mvc;
using eForm.Web.Controllers;

namespace eForm.Web.Public.Controllers
{
    public class AboutController : eFormControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}