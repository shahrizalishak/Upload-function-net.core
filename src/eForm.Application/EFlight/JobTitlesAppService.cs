

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
	[AbpAuthorize(AppPermissions.Pages_JobTitles)]
    public class JobTitlesAppService : eFormAppServiceBase, IJobTitlesAppService
    {
		 private readonly IRepository<JobTitle> _jobTitleRepository;
		 private readonly IJobTitlesExcelExporter _jobTitlesExcelExporter;
		 

		  public JobTitlesAppService(IRepository<JobTitle> jobTitleRepository, IJobTitlesExcelExporter jobTitlesExcelExporter ) 
		  {
			_jobTitleRepository = jobTitleRepository;
			_jobTitlesExcelExporter = jobTitlesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetJobTitleForViewDto>> GetAll(GetAllJobTitlesInput input)
         {
			
			var filteredJobTitles = _jobTitleRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Code.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter);

			var pagedAndFilteredJobTitles = filteredJobTitles
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var jobTitles = from o in pagedAndFilteredJobTitles
                         select new GetJobTitleForViewDto() {
							JobTitle = new JobTitleDto
							{
                                Name = o.Name,
                                Code = o.Code,
                                Id = o.Id
							}
						};

            var totalCount = await filteredJobTitles.CountAsync();

            return new PagedResultDto<GetJobTitleForViewDto>(
                totalCount,
                await jobTitles.ToListAsync()
            );
         }
		 
		 public async Task<GetJobTitleForViewDto> GetJobTitleForView(int id)
         {
            var jobTitle = await _jobTitleRepository.GetAsync(id);

            var output = new GetJobTitleForViewDto { JobTitle = ObjectMapper.Map<JobTitleDto>(jobTitle) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_JobTitles_Edit)]
		 public async Task<GetJobTitleForEditOutput> GetJobTitleForEdit(EntityDto input)
         {
            var jobTitle = await _jobTitleRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetJobTitleForEditOutput {JobTitle = ObjectMapper.Map<CreateOrEditJobTitleDto>(jobTitle)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditJobTitleDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_JobTitles_Create)]
		 protected virtual async Task Create(CreateOrEditJobTitleDto input)
         {
            var jobTitle = ObjectMapper.Map<JobTitle>(input);

			
			if (AbpSession.TenantId != null)
			{
				jobTitle.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _jobTitleRepository.InsertAsync(jobTitle);
         }

		 [AbpAuthorize(AppPermissions.Pages_JobTitles_Edit)]
		 protected virtual async Task Update(CreateOrEditJobTitleDto input)
         {
            var jobTitle = await _jobTitleRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, jobTitle);
         }

		 [AbpAuthorize(AppPermissions.Pages_JobTitles_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _jobTitleRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetJobTitlesToExcel(GetAllJobTitlesForExcelInput input)
         {
			
			var filteredJobTitles = _jobTitleRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter) || e.Code.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CodeFilter),  e => e.Code == input.CodeFilter);

			var query = (from o in filteredJobTitles
                         select new GetJobTitleForViewDto() { 
							JobTitle = new JobTitleDto
							{
                                Name = o.Name,
                                Code = o.Code,
                                Id = o.Id
							}
						 });


            var jobTitleListDtos = await query.ToListAsync();

            return _jobTitlesExcelExporter.ExportToFile(jobTitleListDtos);
         }


    }
}