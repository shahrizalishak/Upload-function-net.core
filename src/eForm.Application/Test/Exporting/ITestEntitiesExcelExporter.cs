using System.Collections.Generic;
using eForm.Test.Dtos;
using eForm.Dto;

namespace eForm.Test.Exporting
{
    public interface ITestEntitiesExcelExporter
    {
        FileDto ExportToFile(List<GetTestEntityForViewDto> testEntities);
    }
}