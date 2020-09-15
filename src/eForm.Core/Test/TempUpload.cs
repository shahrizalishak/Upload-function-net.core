using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace eForm.Test
{
    [Table("TempUpload")]
    public class TempUpload : FullAuditedEntity<Guid>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }

        [Required]
        public virtual byte[] Bytes { get; set; }

        public virtual int? TestEntityId { get; set; }

        public virtual TestEntity TestEntity { get; set; }

        public virtual string Name { get; set; }
        public virtual string ContentType { get; set; }

        public virtual string Remark { get; set; }

        public TempUpload()
        {
            Id = SequentialGuidGenerator.Instance.Create();

        }

        public TempUpload(int? tenantId, byte[] bytes, string contentType, string name)
            : this()
        {
            TenantId = tenantId;
            Bytes = bytes;
            Name = name;
            ContentType = contentType;
        }
    }
}
