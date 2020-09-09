using Abp.Modules;
using Abp.Reflection.Extensions;

namespace eForm
{
    [DependsOn(typeof(eFormXamarinSharedModule))]
    public class eFormXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(eFormXamarinIosModule).GetAssembly());
        }
    }
}