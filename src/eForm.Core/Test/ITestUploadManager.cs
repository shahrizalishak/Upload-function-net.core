using eForm.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eForm.Test
{
    public interface ITestUploadManager
    {
        Task<TestUpload> GetOrNullAsync(Guid id);

        Task SaveAsync(TestUpload file);

        Task DeleteAsync(Guid id);
    }
}
