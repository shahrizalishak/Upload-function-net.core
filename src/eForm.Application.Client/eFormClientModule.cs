using Abp.Modules;
using Abp.Reflection.Extensions;

namespace eForm
{
    public class eFormClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(eFormClientModule).GetAssembly());
        }
    }
}
