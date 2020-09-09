using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using eForm.Authorization;

namespace eForm
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(eFormApplicationSharedModule),
        typeof(eFormCoreModule)
        )]
    public class eFormApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(eFormApplicationModule).GetAssembly());
        }
    }
}