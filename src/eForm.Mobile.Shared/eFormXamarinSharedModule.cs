using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace eForm
{
    [DependsOn(typeof(eFormClientModule), typeof(AbpAutoMapperModule))]
    public class eFormXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(eFormXamarinSharedModule).GetAssembly());
        }
    }
}