using Microsoft.Extensions.Configuration;

namespace eForm.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
