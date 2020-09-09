using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using eForm.Configure;
using eForm.Startup;
using eForm.Test.Base;

namespace eForm.GraphQL.Tests
{
    [DependsOn(
        typeof(eFormGraphQLModule),
        typeof(eFormTestBaseModule))]
    public class eFormGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(eFormGraphQLTestModule).GetAssembly());
        }
    }
}