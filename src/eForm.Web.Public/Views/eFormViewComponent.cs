using Abp.AspNetCore.Mvc.ViewComponents;

namespace eForm.Web.Public.Views
{
    public abstract class eFormViewComponent : AbpViewComponent
    {
        protected eFormViewComponent()
        {
            LocalizationSourceName = eFormConsts.LocalizationSourceName;
        }
    }
}