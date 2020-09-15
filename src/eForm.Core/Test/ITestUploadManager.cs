using Abp.Application.Services.Dto;
using eForm.Storage;
using eForm.Test.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eForm.Test
{
    public interface ITestUploadManager
    {
        Task<TestUpload> GetOrNullAsync(Guid id);

        Task<TempUpload> GetOrNullAsyncReal(Guid id);

        Task SaveAsync(TestUpload file);

        Task DeleteAsyncTemp(Guid id);

        Task DeleteAsync(Guid id);

    }
}
