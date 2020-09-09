

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using eForm.EFlight.Dtos;
using eForm.Dto;
using Abp.Application.Services.Dto;
using eForm.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace eForm.EFlight
{
	[AbpAuthorize(AppPermissions.Pages_Purposes)]
    public class PurposesAppService : eFormAppServiceBase, IPurposesAppService
    {
		 private readonly IRepository<Purpose> _purposeRepository;
		 

		  public PurposesAppService(IRepository<Purpose> purposeRepository ) 
		  {
			_purposeRepository = purposeRepository;
			
		  }

		 public async Task<PagedResultDto<GetPurposeForViewDto>> GetAll(GetAllPurposesInput input)
         {
			
			var filteredPurposes = _purposeRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter);

			var pagedAndFilteredPurposes = filteredPurposes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var purposes = from o in pagedAndFilteredPurposes
                         select new GetPurposeForViewDto() {
							Purpose = new PurposeDto
							{
                                Name = o.Name,
                                Id = o.Id
							}
						};

            var totalCount = await filteredPurposes.CountAsync();

            return new PagedResultDto<GetPurposeForViewDto>(
                totalCount,
                await purposes.ToListAsync()
            );
         }
		 
		 public async Task<GetPurposeForViewDto> GetPurposeForView(int id)
         {
            var purpose = await _purposeRepository.GetAsync(id);

            var output = new GetPurposeForViewDto { Purpose = ObjectMapper.Map<PurposeDto>(purpose) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Purposes_Edit)]
		 public async Task<GetPurposeForEditOutput> GetPurposeForEdit(EntityDto input)
         {
            var purpose = await _purposeRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPurposeForEditOutput {Purpose = ObjectMapper.Map<CreateOrEditPurposeDto>(purpose)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPurposeDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Purposes_Create)]
		 protected virtual async Task Create(CreateOrEditPurposeDto input)
         {
            var purpose = ObjectMapper.Map<Purpose>(input);

			
			if (AbpSession.TenantId != null)
			{
				purpose.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _purposeRepository.InsertAsync(purpose);
         }

		 [AbpAuthorize(AppPermissions.Pages_Purposes_Edit)]
		 protected virtual async Task Update(CreateOrEditPurposeDto input)
         {
            var purpose = await _purposeRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, purpose);
         }

		 [AbpAuthorize(AppPermissions.Pages_Purposes_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _purposeRepository.DeleteAsync(input.Id);
         } 
    }
}