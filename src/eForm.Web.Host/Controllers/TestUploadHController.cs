using System;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using eForm.Test;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace eForm.Web.Controllers
{
    [AllowAnonymous]
    public class TestUploadHController : TestUploadController
    {
        protected readonly ITestUploadManager _testUploadManager;
        public TestUploadHController(ITestUploadManager testUploadManager) :
            base(testUploadManager)
        {
        }

        public async Task<ActionResult> DownloadFileH(Guid fileId, string contentType, string fileName)
        {
            var fileObject = await _testUploadManager.GetOrNullAsync(fileId);
            if (fileObject == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            return File(fileObject.Bytes, contentType, fileName);
        }

        public async Task DeleteUploadedObject(Guid fileId)
        {
            await _testUploadManager.DeleteAsync(fileId).ConfigureAwait(false);
        }
    }
}
