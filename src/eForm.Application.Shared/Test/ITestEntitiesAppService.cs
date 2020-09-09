using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using eForm.Test.Dtos;
using eForm.Dto;


namespace eForm.Test
{
    public interface ITestEntitiesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetTestEntityForViewDto>> GetAll(GetAllTestEntitiesInput input);

        Task<GetTestEntityForViewDto> GetTestEntityForView(int id);

		Task<GetTestEntityForEditOutput> GetTestEntityForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditTestEntityDto input);

		Task Delete(EntityDto input);

		Task<FileDto> GetTestEntitiesToExcel(GetAllTestEntitiesForExcelInput input);

		
    }
}