using System.Collections.Generic;
using eForm.EFlight.Dtos;
using eForm.Dto;

namespace eForm.EFlight.Exporting
{
    public interface ITravelAgentsExcelExporter
    {
        FileDto ExportToFile(List<GetTravelAgentForViewDto> travelAgents);
    }
}