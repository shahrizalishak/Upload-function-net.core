using Abp.Modules;
using Abp.Reflection.Extensions;

namespace eForm
{
    [DependsOn(typeof(eFormCoreSharedModule))]
    public class eFormApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(eFormApplicationSharedModule).GetAssembly());
        }
    }
}