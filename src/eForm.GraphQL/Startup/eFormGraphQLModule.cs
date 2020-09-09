using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace eForm.Startup
{
    [DependsOn(typeof(eFormCoreModule))]
    public class eFormGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(eFormGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}