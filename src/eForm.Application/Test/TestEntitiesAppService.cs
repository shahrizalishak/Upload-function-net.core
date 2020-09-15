

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
		private readonly IRepository<TempUpload, Guid> _tempUploadRepository;

		public TestEntitiesAppService(IRepository<TestEntity> testEntityRepository, 
			  ITestEntitiesExcelExporter testEntitiesExcelExporter,
			  ITestUploadManager testUploadManager,
			  IRepository<TestUpload, Guid> testUploadRepository,
			  IRepository<TempUpload, Guid> tempUploadRepository) 
		  {
			_testEntityRepository = testEntityRepository;
			_testEntitiesExcelExporter = testEntitiesExcelExporter;
			_testUploadManager = testUploadManager;
			_testUploadRepository = testUploadRepository;
			_tempUploadRepository = tempUploadRepository;

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
            //var testEntity = await _testEntityRepository.GetAsync(id);
			var testEntity = await _testEntityRepository.GetAll()
			   .Include(e => e.TempUpload)
				.Where(e => e.Id == id)
				.FirstOrDefaultAsync();

			var output = new GetTestEntityForViewDto { TestEntity = ObjectMapper.Map<TestEntityDto>(testEntity) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_TestEntities_Edit)]
		 public async Task<GetTestEntityForEditOutput> GetTestEntityForEdit(EntityDto input)
         {
			//var testEntity = await _testEntityRepository.FirstOrDefaultAsync(input.Id);
			var testEntity = await _testEntityRepository.GetAll()
			   .Include(e => e.TempUpload)
				.Where(e => e.Id == input.Id)
				.FirstOrDefaultAsync();


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
			if (ListFileID != null)
			{
				await UpdateFile(TestId, ListFileID);
			}
			

		}

		//Insert TestID into TestUpload table
		protected virtual async Task UpdateFile(int? testId, IList<String> listFileId)
		{
				foreach (var att in listFileId)
				{
				var testFile = new EditTestUploadDto
				{
					Id = Guid.Parse(att),
					TestId = testId
				};
				var fileObject = await _testUploadManager.GetOrNullAsync(Guid.Parse(att));
				ObjectMapper.Map(testFile, fileObject);
				
				await CopyTempFile(fileObject);
				await DeleteFile(fileObject.Id);
			}

			   
		}


		protected virtual async Task CopyTempFile(TestUpload listFileId)
		{
			var newFile = new TempUpload
			{
				Id = listFileId.Id,
				Bytes = listFileId.Bytes,
				Name = listFileId.Name,
				ContentType = listFileId.ContentType,
				TestEntityId = listFileId.TestId,
				TenantId = listFileId.TenantId
			};
			var fileObjectne = ObjectMapper.Map<TempUpload>(newFile);
			await _tempUploadRepository.InsertAsync(fileObjectne);
		}


		public async Task DeleteFile(Guid GuidFfile)
		{
			await _testUploadRepository.DeleteAsync(GuidFfile);
		}

		[AbpAuthorize(AppPermissions.Pages_TestEntities_Edit)]
		 protected virtual async Task Update(CreateOrEditTestEntityDto input)
         {
			var testEntity = await _testEntityRepository.FirstOrDefaultAsync((int)input.Id);
			ObjectMapper.Map(input, testEntity);

			var ListFileID = input.TestUploadListID;
			if (ListFileID != null)
			{
				await UpdateFile(input.Id, ListFileID);
			}
		}

		 [AbpAuthorize(AppPermissions.Pages_TestEntities_Delete)]
         public async Task Delete(EntityDto input)
         {
			var testEntity = await _testEntityRepository.GetAll()
			   .Include(e => e.TempUpload)
				.Where(e => e.Id == input.Id)
				.FirstOrDefaultAsync();
			foreach (var att in testEntity.TempUpload)
			{
				await _testUploadManager.DeleteAsync(att.Id);
			}
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

		public async Task DeleteAttachmentTemp(string input)
		{
			await _testUploadManager.DeleteAsyncTemp(Guid.Parse(input)).ConfigureAwait(false);
		}

	}
}