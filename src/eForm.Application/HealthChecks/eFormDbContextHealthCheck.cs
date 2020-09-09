using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using eForm.EntityFrameworkCore;

namespace eForm.HealthChecks
{
    public class eFormDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public eFormDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("eFormDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("eFormDbContext could not connect to database"));
        }
    }
}
