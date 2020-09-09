using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using eForm.EFlight.Dtos;
using eForm.Dto;


namespace eForm.EFlight
{
    public interface IJobTitlesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetJobTitleForViewDto>> GetAll(GetAllJobTitlesInput input);

        Task<GetJobTitleForViewDto> GetJobTitleForView(int id);

		Task<GetJobTitleForEditOutput> GetJobTitleForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditJobTitleDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetJobTitlesToExcel(GetAllJobTitlesForExcelInput input);

		
    }
}