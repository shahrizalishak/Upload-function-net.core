

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using eForm.Test.Exporting;
using eForm.Test.Dtos;
using eForm.Dto;
using Abp.Application.Services.Dto;
using eForm.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using eForm.Storage;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Collections;

namespace eForm.Test
{
	[AbpAuthorize(AppPermissions.Pages_TestEntities)]
    public class TestEntitiesAppService : eFormAppServiceBase, ITestEntitiesAppService
    {
		private readonly IRepository<TestEntity> _testEntityRepository;
		private readonly ITestEntitiesExcelExporter _testEntitiesExcelExporter;
		private readonly ITestUploadManager _testUploadManager;
		private readonly IRepository<TestUpload, Guid> _testUploadRepository;

		public TestEntitiesAppService(IRepository<TestEntity> testEntityRepository, 
			  ITestEntitiesExcelExporter testEntitiesExcelExporter,
			  ITestUploadManager testUploadManager,
			  IRepository<TestUpload, Guid> testUploadRepository) 
		  {
			_testEntityRepository = testEntityRepository;
			_testEntitiesExcelExporter = testEntitiesExcelExporter;
			_testUploadManager = testUploadManager;
			_testUploadRepository = testUploadRepository;

		  }

		 public async Task<PagedResultDto<GetTestEntityForViewDto>> GetAll(GetAllTestEntitiesInput input)
         {
			
			var filteredTestEntities = _testEntityRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter);

			var pagedAndFilteredTestEntities = filteredTestEntities
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var testEntities = from o in pagedAndFilteredTestEntities
                         select new GetTestEntityForViewDto() {
							TestEntity = new TestEntityDto
							{
                                Name = o.Name,
                                Id = o.Id
							}
						};

            var totalCount = await filteredTestEntities.CountAsync();

            return new PagedResultDto<GetTestEntityForViewDto>(
                totalCount,
                await testEntities.ToListAsync()
            );
         }
		 
		 public async Task<GetTestEntityForViewDto> GetTestEntityForView(int id)
         {
            var testEntity = await _testEntityRepository.GetAsync(id);

            var output = new GetTestEntityForViewDto { TestEntity = ObjectMapper.Map<TestEntityDto>(testEntity) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_TestEntities_Edit)]
		 public async Task<GetTestEntityForEditOutput> GetTestEntityForEdit(EntityDto input)
         {
            var testEntity = await _testEntityRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetTestEntityForEditOutput {TestEntity = ObjectMapper.Map<CreateOrEditTestEntityDto>(testEntity)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditTestEntityDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

	

		[AbpAuthorize(AppPermissions.Pages_TestEntities_Create)]
		 protected virtual async Task Create(CreateOrEditTestEntityDto input)
         {
            var testEntity = ObjectMapper.Map<TestEntity>(input);

			var TestId = await _testEntityRepository.InsertAndGetIdAsync(testEntity);
			var ListFileID = input.TestUploadListID;
			await UpdateFile(TestId, ListFileID);

		}

		//Insert TestID into TestUpload table
		[AbpAuthorize(AppPermissions.Pages_TestEntities_Create)]
		protected virtual async Task UpdateFile(int testId, IList<string> listFileId)
		{
			var testFile = new EditTestUploadDto();
			testFile.TestId = testId;

				foreach (var att in listFileId)
				{
					var fileObject = await _testUploadManager.GetOrNullAsync(Guid.Parse(att));
					ObjectMapper.Map(testFile, fileObject);
				}	
		}

		 [AbpAuthorize(AppPermissions.Pages_TestEntities_Edit)]
		 protected virtual async Task Update(CreateOrEditTestEntityDto input)
         {
            var testEntity = await _testEntityRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, testEntity);
         }

		 [AbpAuthorize(AppPermissions.Pages_TestEntities_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _testEntityRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetTestEntitiesToExcel(GetAllTestEntitiesForExcelInput input)
         {
			
			var filteredTestEntities = _testEntityRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Name.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter);

			var query = (from o in filteredTestEntities
                         select new GetTestEntityForViewDto() { 
							TestEntity = new TestEntityDto
							{
                                Name = o.Name,
                                Id = o.Id
							}
						 });


            var testEntityListDtos = await query.ToListAsync();

            return _testEntitiesExcelExporter.ExportToFile(testEntityListDtos);
         }

		public async Task<int> GetTestID(CreateOrEditTestEntityDto input)
		{
			var testEntity = ObjectMapper.Map<TestEntity>(input);

			var id = await _testEntityRepository.InsertAndGetIdAsync(testEntity);

			return id;
		}

		//Upload
		public async Task DeleteAttachment(string input)
		{
			await _testUploadManager.DeleteAsync(Guid.Parse(input)).ConfigureAwait(false);
		}

	}
}