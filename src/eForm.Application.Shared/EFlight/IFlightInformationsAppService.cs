using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using eForm.EFlight.Dtos;
using eForm.Dto;


namespace eForm.EFlight
{
    public interface IFlightInformationsAppService : IApplicationService 
    {
        Task<PagedResultDto<GetFlightInformationForViewDto>> GetAll(GetAllFlightInformationsInput input);

        Task<GetFlightInformationForViewDto> GetFlightInformationForView(int id);

		Task<GetFlightInformationForEditOutput> GetFlightInformationForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditFlightInformationDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetFlightInformationsToExcel(GetAllFlightInformationsForExcelInput input);

		
    }
}