using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Dependency;
using Abp.Domain.Repositories;
using eForm.Storage;
using eForm.Test.Dtos;

namespace eForm.Test
{
    public class DbTestUploadManager : ITestUploadManager, ITransientDependency
    {
        private readonly IRepository<TestUpload, Guid> _testUploadRepository;
        private readonly IRepository<TempUpload, Guid> _tempUploadRepository;

        public DbTestUploadManager(IRepository<TestUpload, Guid> testUploadRepository,
            IRepository<TempUpload, Guid> tempUploadRepository)
        {
            _testUploadRepository = testUploadRepository;
            _tempUploadRepository = tempUploadRepository;
        }

        public Task<TestUpload> GetOrNullAsync(Guid id)
        {
            return _testUploadRepository.FirstOrDefaultAsync(id);
        }

        public Task<TempUpload> GetOrNullAsyncReal(Guid id)
        {
            return _tempUploadRepository.FirstOrDefaultAsync(id);
        }


        public Task SaveAsync(TestUpload file)
        {
            return _testUploadRepository.InsertAsync(file);
        }

        public Task DeleteAsyncTemp(Guid id)
        {
            return _testUploadRepository.DeleteAsync(id);
        }

        public Task DeleteAsync(Guid id)
        {
            return _tempUploadRepository.DeleteAsync(id);
        }
    }
}
