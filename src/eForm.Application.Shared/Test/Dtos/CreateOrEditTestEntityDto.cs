
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace eForm.Test.Dtos
{
    public class CreateOrEditTestEntityDto : EntityDto<int?>
    {

		public string Name { get; set; }

        public ICollection<TestUploadDto> TestUpload { get; set; }

        public IList<string> TestUploadListID { get; set; }



    }
}