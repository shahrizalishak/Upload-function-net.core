using Abp.Modules;
using Abp.Reflection.Extensions;

namespace eForm
{
    [DependsOn(typeof(eFormXamarinSharedModule))]
    public class eFormXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(eFormXamarinAndroidModule).GetAssembly());
        }
    }
}