using System.Collections.Generic;
using eForm.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace eForm.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
