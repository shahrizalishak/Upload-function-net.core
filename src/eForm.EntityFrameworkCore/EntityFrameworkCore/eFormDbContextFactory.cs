using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using eForm.Configuration;
using eForm.Web;

namespace eForm.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class eFormDbContextFactory : IDesignTimeDbContextFactory<eFormDbContext>
    {
        public eFormDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<eFormDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            eFormDbContextConfigurer.Configure(builder, configuration.GetConnectionString(eFormConsts.ConnectionStringName));

            return new eFormDbContext(builder.Options);
        }
    }
}