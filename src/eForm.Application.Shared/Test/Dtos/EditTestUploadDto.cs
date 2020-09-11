using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace eForm.Test.Dtos
{
    public class EditTestUploadDto : EntityDto<Guid?>
    {
        public int TestId { get; set; }
    }
}
