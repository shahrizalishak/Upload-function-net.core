using Abp.Application.Services.Dto;
using System;

namespace eForm.EFlight.Dtos
{
    public class GetAllFlightsForExcelInput
    {
		public string Filter { get; set; }

		public string DetailFilter { get; set; }

		public string NameFilter { get; set; }

		public string NRICFilter { get; set; }

		public string StaffIDFilter { get; set; }

		public string PositionFilter { get; set; }

		public string EmailFilter { get; set; }

		public string PhoneNoFilter { get; set; }

		public string MembershipNoFilter { get; set; }

		public string ValidationNameFilter { get; set; }

		public string ValidationPhoneNoFilter { get; set; }

		public string ValidationPositionFilter { get; set; }

		public DateTime? MaxValidationDateFilter { get; set; }
		public DateTime? MinValidationDateFilter { get; set; }

		public int ValidationFilter { get; set; }

		public string ApprovalNameFilter { get; set; }

		public string ApprovalPositionFilter { get; set; }

		public DateTime? MaxApprovalDateFilter { get; set; }
		public DateTime? MinApprovalDateFilter { get; set; }

		public int ApprovalFilter { get; set; }


		 public string TravelAgentNameFilter { get; set; }

		 		 public string PurposeNameFilter { get; set; }

		 		 public string JobTitleNameFilter { get; set; }

		 
    }
}