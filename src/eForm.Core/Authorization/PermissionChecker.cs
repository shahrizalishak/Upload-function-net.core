using Abp.Authorization;
using eForm.Authorization.Roles;
using eForm.Authorization.Users;

namespace eForm.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
