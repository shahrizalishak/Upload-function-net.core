using eForm.EFlight;
using eForm.EFlight;
using eForm.EFlight;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace eForm.EFlight
{
	[Table("Flights")]
    public class Flight : Entity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual string Detail { get; set; }
		
		public virtual string Name { get; set; }
		
		public virtual string NRIC { get; set; }
		
		public virtual string StaffID { get; set; }
		
		public virtual string Position { get; set; }
		
		public virtual string Email { get; set; }
		
		public virtual string PhoneNo { get; set; }
		
		public virtual string MembershipNo { get; set; }
		
		public virtual string ValidationName { get; set; }
		
		public virtual string ValidationPhoneNo { get; set; }
		
		public virtual string ValidationPosition { get; set; }
		
		public virtual DateTime ValidationDate { get; set; }
		
		public virtual bool Validation { get; set; }
		
		public virtual string ApprovalName { get; set; }
		
		public virtual string ApprovalPosition { get; set; }
		
		public virtual DateTime ApprovalDate { get; set; }
		
		public virtual bool Approval { get; set; }
		

		public virtual int? TravelAgentId { get; set; }
		
        [ForeignKey("TravelAgentId")]
		public TravelAgent TravelAgentFk { get; set; }
		
		public virtual int? PurposeId { get; set; }
		
        [ForeignKey("PurposeId")]
		public Purpose PurposeFk { get; set; }
		
		public virtual int? JobTitleId { get; set; }
		
        [ForeignKey("JobTitleId")]
		public JobTitle JobTitleFk { get; set; }
		
    }
}