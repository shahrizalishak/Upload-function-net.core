using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace eForm.Test.Dtos
{
    public class TempUploadDto : FullAuditedEntityDto<Guid>
    {
        public byte[] Bytes { get; set; }

        public int TestId { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public string Remark { get; set; }

        public int TenantId { get; set; }
    }
}
