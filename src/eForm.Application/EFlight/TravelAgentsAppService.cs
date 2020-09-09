

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using eForm.EFlight.Exporting;
using eForm.EFlight.Dtos;
using eForm.Dto;
using Abp.Application.Services.Dto;
using eForm.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace eForm.EFlight
{
	[AbpAuthorize(AppPermissions.Pages_TravelAgents)]
    public class TravelAgentsAppService : eFormAppServiceBase, ITravelAgentsAppService
    {
		 private readonly IRepository<TravelAgent> _travelAgentRepository;
		 private readonly ITravelAgentsExcelExporter _travelAgentsExcelExporter;
		 

		  public TravelAgentsAppService(IRepository<TravelAgent> travelAgentRepository, ITravelAgentsExcelExporter travelAgentsExcelExporter ) 
		  {
			_travelAgentRepository = travelAgentRepository;
			_travelAgentsExcelExporter = travelAgentsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetTravelAgentForViewDto>> GetAll(GetAllTravelAgentsInput input)
         {
			
			var filteredTravelAgents = _travelAgentRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter),  e => e.Email == input.EmailFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter),  e => e.PhoneNo == input.PhoneNoFilter);

			var pagedAndFilteredTravelAgents = filteredTravelAgents
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var travelAgents = from o in pagedAndFilteredTravelAgents
                         select new GetTravelAgentForViewDto() {
							TravelAgent = new TravelAgentDto
							{
                                Name = o.Name,
                                Email = o.Email,
                                PhoneNo = o.PhoneNo,
                                Id = o.Id
							}
						};

            var totalCount = await filteredTravelAgents.CountAsync();

            return new PagedResultDto<GetTravelAgentForViewDto>(
                totalCount,
                await travelAgents.ToListAsync()
            );
         }
		 
		 public async Task<GetTravelAgentForViewDto> GetTravelAgentForView(int id)
         {
            var travelAgent = await _travelAgentRepository.GetAsync(id);

            var output = new GetTravelAgentForViewDto { TravelAgent = ObjectMapper.Map<TravelAgentDto>(travelAgent) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_TravelAgents_Edit)]
		 public async Task<GetTravelAgentForEditOutput> GetTravelAgentForEdit(EntityDto input)
         {
            var travelAgent = await _travelAgentRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetTravelAgentForEditOutput {TravelAgent = ObjectMapper.Map<CreateOrEditTravelAgentDto>(travelAgent)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditTravelAgentDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_TravelAgents_Create)]
		 protected virtual async Task Create(CreateOrEditTravelAgentDto input)
         {
            var travelAgent = ObjectMapper.Map<TravelAgent>(input);

			
			if (AbpSession.TenantId != null)
			{
				travelAgent.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _travelAgentRepository.InsertAsync(travelAgent);
         }

		 [AbpAuthorize(AppPermissions.Pages_TravelAgents_Edit)]
		 protected virtual async Task Update(CreateOrEditTravelAgentDto input)
         {
            var travelAgent = await _travelAgentRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, travelAgent);
         }

		 [AbpAuthorize(AppPermissions.Pages_TravelAgents_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _travelAgentRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetTravelAgentsToExcel(GetAllTravelAgentsForExcelInput input)
         {
			
			var filteredTravelAgents = _travelAgentRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter),  e => e.Email == input.EmailFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter),  e => e.PhoneNo == input.PhoneNoFilter);

			var query = (from o in filteredTravelAgents
                         select new GetTravelAgentForViewDto() { 
							TravelAgent = new TravelAgentDto
							{
                                Name = o.Name,
                                Email = o.Email,
                                PhoneNo = o.PhoneNo,
                                Id = o.Id
							}
						 });


            var travelAgentListDtos = await query.ToListAsync();

            return _travelAgentsExcelExporter.ExportToFile(travelAgentListDtos);
         }


    }
}