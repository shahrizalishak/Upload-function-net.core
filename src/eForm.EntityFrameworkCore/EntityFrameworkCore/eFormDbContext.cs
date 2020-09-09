using eForm.Test;
using eForm.EFlight;
using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using eForm.Authorization.Roles;
using eForm.Authorization.Users;
using eForm.Chat;
using eForm.Editions;
using eForm.Friendships;
using eForm.MultiTenancy;
using eForm.MultiTenancy.Accounting;
using eForm.MultiTenancy.Payments;
using eForm.Storage;

namespace eForm.EntityFrameworkCore
{
    public class eFormDbContext : AbpZeroDbContext<Tenant, Role, User, eFormDbContext>, IAbpPersistedGrantDbContext
    {
        public virtual DbSet<TestEntity> TestEntities { get; set; }

        public virtual DbSet<Flight> Flights { get; set; }

        public virtual DbSet<TravelAgent> TravelAgents { get; set; }

        public virtual DbSet<FlightInformation> FlightInformations { get; set; }

        public virtual DbSet<JobTitle> JobTitles { get; set; }

        public virtual DbSet<Purpose> Purposes { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<TestUpload> TestUpload { get; set; }

        public virtual DbSet<TempUpload> TempUpload { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public eFormDbContext(DbContextOptions<eFormDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
           
           
           
           
            modelBuilder.Entity<Flight>(f =>
            {
                f.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<TravelAgent>(t =>
            {
                t.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<FlightInformation>(f =>
            {
                f.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<JobTitle>(j =>
            {
                j.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<Purpose>(p =>
            {
                p.HasIndex(e => new { e.TenantId });
            });
 modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });
modelBuilder.Entity<TestUpload>(c =>
            {
                c.HasIndex(e => new { e.TenantId });
            });
modelBuilder.Entity<TempUpload>(d =>
            {
                d.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique();
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
