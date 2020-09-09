using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using eForm.DemoUiComponents.Dto;
using eForm.Storage;
using eForm.Test.Dtos;
using eForm.Test;
using Abp.Auditing;
using System;
using System.Net;

namespace eForm.Web.Controllers
{
    [AbpMvcAuthorize]
    public class TestUploadController : eFormControllerBase
    {
        private readonly ITestUploadManager _testUploadManager;

        public TestUploadController(ITestUploadManager testUploadManager) 
        {
            _testUploadManager = testUploadManager;
        }



        public async Task<ActionResult> DownloadFile(Guid id, string contentType, string fileName)
        {
            var fileObject = await _testUploadManager.GetOrNullAsync(id);
            if (fileObject == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            return File(fileObject.Bytes, contentType, fileName);
        }

        public async Task DeleteFile(Guid fileId)
        {
            await _testUploadManager.DeleteAsync(fileId).ConfigureAwait(false);
        }


        [HttpPost]
        public async Task<JsonResult> UploadFiles()
        {
            try
            {
                var files = Request.Form.Files;

                //Check input
                if (files == null)
                {
                    throw new UserFriendlyException(L("File_Empty_Error"));
                }

                List<TestUploadDto> filesOutput = new List<TestUploadDto>();

                foreach (var file in files)
                {
                    if (file.Length > 1048576) //1MB
                    {
                        throw new UserFriendlyException(L("File_SizeLimit_Error"));
                    }

                    byte[] fileBytes;
                    using (var stream = file.OpenReadStream())
                    {
                        fileBytes = stream.GetAllBytes();
                    }

                    var fileObject = new TestUpload(AbpSession.TenantId, fileBytes, file.ContentType, file.FileName);
                    await _testUploadManager.SaveAsync(fileObject);

                    filesOutput.Add(new TestUploadDto
                    {
                        Id = fileObject.Id,
                        Name = file.FileName,
                        ContentType = file.ContentType
                    });
                }

                return Json(new AjaxResponse(filesOutput));
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

    }
}
