using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using eForm.EFlight.Dtos;
using eForm.Dto;


namespace eForm.EFlight
{
    public interface IPurposesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPurposeForViewDto>> GetAll(GetAllPurposesInput input);

        Task<GetPurposeForViewDto> GetPurposeForView(int id);

		Task<GetPurposeForEditOutput> GetPurposeForEdit(EntityDto input);

		Task CreateOrEdit(CreateOrEditPurposeDto input);

		Task Delete(EntityDto input);

		
    }
}