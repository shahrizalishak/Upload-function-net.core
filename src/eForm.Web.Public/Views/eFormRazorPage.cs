using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace eForm.Web.Public.Views
{
    public abstract class eFormRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected eFormRazorPage()
        {
            LocalizationSourceName = eFormConsts.LocalizationSourceName;
        }
    }
}
