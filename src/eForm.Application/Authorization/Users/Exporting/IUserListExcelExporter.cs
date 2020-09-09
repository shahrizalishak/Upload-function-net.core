using System.Collections.Generic;
using eForm.Authorization.Users.Dto;
using eForm.Dto;

namespace eForm.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}