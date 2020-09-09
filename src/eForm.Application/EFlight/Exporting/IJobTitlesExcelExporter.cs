using System.Collections.Generic;
using eForm.EFlight.Dtos;
using eForm.Dto;

namespace eForm.EFlight.Exporting
{
    public interface IJobTitlesExcelExporter
    {
        FileDto ExportToFile(List<GetJobTitleForViewDto> jobTitles);
    }
}