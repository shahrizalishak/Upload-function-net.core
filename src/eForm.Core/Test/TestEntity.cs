using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp;
using System.Collections.Generic;
using eForm.Test.Dtos;

namespace eForm.Test
{
	[Table("TestEntities")]
    public class TestEntity : Entity 
    {

		public virtual string Name { get; set; }

        public virtual ICollection<TestUpload> TestUpload { get; set; }


    }
}