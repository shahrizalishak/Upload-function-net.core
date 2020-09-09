using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using eForm.Storage;

namespace eForm.Test
{
    public class DbTestUploadManager : ITestUploadManager, ITransientDependency
    {
        private readonly IRepository<TestUpload, Guid> _testUploadRepository;

        public DbTestUploadManager(IRepository<TestUpload, Guid> testUploadRepository)
        {
            _testUploadRepository = testUploadRepository;
        }

        public Task<TestUpload> GetOrNullAsync(Guid id)
        {
            return _testUploadRepository.FirstOrDefaultAsync(id);
        }

        public Task SaveAsync(TestUpload file)
        {
            return _testUploadRepository.InsertAsync(file);
        }

        public Task DeleteAsync(Guid id)
        {
            return _testUploadRepository.DeleteAsync(id);
        }
    }
}
