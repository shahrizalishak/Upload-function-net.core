
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace eForm.EFlight.Dtos
{
    public class CreateOrEditFlightDto : EntityDto<int?>
    {

		public string Detail { get; set; }
		
		
		public string Name { get; set; }
		
		
		public string NRIC { get; set; }
		
		
		public string StaffID { get; set; }
		
		
		public string Position { get; set; }
		
		
		public string Email { get; set; }
		
		
		public string PhoneNo { get; set; }
		
		
		public string MembershipNo { get; set; }
		
		
		public string ValidationName { get; set; }
		
		
		public string ValidationPhoneNo { get; set; }
		
		
		public string ValidationPosition { get; set; }
		
		
		public DateTime ValidationDate { get; set; }
		
		
		public bool Validation { get; set; }
		
		
		public string ApprovalName { get; set; }
		
		
		public string ApprovalPosition { get; set; }
		
		
		public DateTime ApprovalDate { get; set; }
		
		
		public bool Approval { get; set; }
		
		
		 public int? TravelAgentId { get; set; }
		 
		 		 public int? PurposeId { get; set; }
		 
		 		 public int? JobTitleId { get; set; }
		 
		 
    }
}