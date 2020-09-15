
using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace eForm.Test.Dtos
{
    public class TestEntityDto : EntityDto
    {
		public string Name { get; set; }

        public ICollection<TempUploadDto> TempUpload { get; set; }

        public IList<string> TempUploadListID { get; set; }

    }
}