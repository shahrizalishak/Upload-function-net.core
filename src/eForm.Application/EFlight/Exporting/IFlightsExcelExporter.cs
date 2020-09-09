using System.Collections.Generic;
using eForm.EFlight.Dtos;
using eForm.Dto;

namespace eForm.EFlight.Exporting
{
    public interface IFlightsExcelExporter
    {
        FileDto ExportToFile(List<GetFlightForViewDto> flights);
    }
}