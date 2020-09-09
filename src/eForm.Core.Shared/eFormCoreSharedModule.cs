using Abp.Modules;
using Abp.Reflection.Extensions;

namespace eForm
{
    public class eFormCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(eFormCoreSharedModule).GetAssembly());
        }
    }
}