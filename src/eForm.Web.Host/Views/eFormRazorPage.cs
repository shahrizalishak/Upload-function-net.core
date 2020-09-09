using Abp.AspNetCore.Mvc.Views;

namespace eForm.Web.Views
{
    public abstract class eFormRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected eFormRazorPage()
        {
            LocalizationSourceName = eFormConsts.LocalizationSourceName;
        }
    }
}
