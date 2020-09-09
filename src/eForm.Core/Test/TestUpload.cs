using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace eForm.Test
{
    [Table("TestUpload")]
    public class TestUpload : FullAuditedEntity<Guid>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }

        [Required]
        public virtual byte[] Bytes { get; set; }

        public virtual int? TestId { get; set; }
        public virtual TestEntity TestName { get; set; }

        public virtual string Name { get; set; }
        public virtual string ContentType { get; set; }

        public virtual string Remark { get; set; }

        public TestUpload()
        {
            Id = SequentialGuidGenerator.Instance.Create();
            
        }

        public TestUpload(int? tenantId, byte[] bytes, string contentType, string name)
            : this()
        {
            TenantId = tenantId;
            Bytes = bytes;
            Name = name;
            ContentType = contentType;
        }
    }
}
