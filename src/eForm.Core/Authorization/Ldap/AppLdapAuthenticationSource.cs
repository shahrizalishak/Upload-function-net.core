using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using eForm.Authorization.Users;
using eForm.MultiTenancy;

namespace eForm.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}