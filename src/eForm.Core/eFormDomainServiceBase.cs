using Abp.Domain.Services;

namespace eForm
{
    public abstract class eFormDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected eFormDomainServiceBase()
        {
            LocalizationSourceName = eFormConsts.LocalizationSourceName;
        }
    }
}
