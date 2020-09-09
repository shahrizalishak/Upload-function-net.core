using System.Collections.Generic;
using Abp;
using eForm.Chat.Dto;
using eForm.Dto;

namespace eForm.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
