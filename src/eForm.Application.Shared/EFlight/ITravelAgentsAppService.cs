using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using eForm.EFlight.Dtos;
using eForm.Dto;


namespace eForm.EFlight
{
    public interface ITravelAgentsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetTravelAgentForViewDto>> GetAll(GetAllTravelAgentsInput input);

        Task<GetTravelAgentForViewDto> GetTravelAgentForView(int id);

		Task<GetTravelAgentForEditOutput> GetTravelAgentForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditTravelAgentDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetTravelAgentsToExcel(GetAllTravelAgentsForExcelInput input);

		
    }
}