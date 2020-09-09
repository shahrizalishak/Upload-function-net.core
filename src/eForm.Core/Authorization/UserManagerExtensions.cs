﻿using System.Threading.Tasks;
using Abp.Authorization.Users;
using eForm.Authorization.Users;

namespace eForm.Authorization
{
    public static class UserManagerExtensions
    {
        public static async Task<User> GetAdminAsync(this UserManager userManager)
        {
            return await userManager.FindByNameAsync(AbpUserBase.AdminUserName);
        }
    }
}
