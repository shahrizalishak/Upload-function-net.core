using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace eForm.EntityFrameworkCore
{
    public static class eFormDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<eFormDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<eFormDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}