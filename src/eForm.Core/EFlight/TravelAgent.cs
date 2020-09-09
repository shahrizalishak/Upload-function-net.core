﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace eForm.EFlight
{
	[Table("TravelAgents")]
    public class TravelAgent : Entity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string Name { get; set; }
		
		public virtual string Email { get; set; }
		
		public virtual string PhoneNo { get; set; }
		

    }
}