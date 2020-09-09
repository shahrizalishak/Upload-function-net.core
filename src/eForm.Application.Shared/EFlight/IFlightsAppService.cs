using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using eForm.EFlight.Dtos;
using eForm.Dto;


namespace eForm.EFlight
{
    public interface IFlightsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetFlightForViewDto>> GetAll(GetAllFlightsInput input);

        Task<GetFlightForViewDto> GetFlightForView(int id);

		Task<GetFlightForEditOutput> GetFlightForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditFlightDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetFlightsToExcel(GetAllFlightsForExcelInput input);

		
		Task<PagedResultDto<FlightTravelAgentLookupTableDto>> GetAllTravelAgentForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<FlightPurposeLookupTableDto>> GetAllPurposeForLookupTable(GetAllForLookupTableInput input);
		
		Task<PagedResultDto<FlightJobTitleLookupTableDto>> GetAllJobTitleForLookupTable(GetAllForLookupTableInput input);
		
    }
}