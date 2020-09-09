using System.Collections.Generic;
using eForm.Authorization.Users.Importing.Dto;
using eForm.Dto;

namespace eForm.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
