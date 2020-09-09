using Abp.Application.Services.Dto;
using System;

namespace eForm.EFlight.Dtos
{
    public class GetAllTravelAgentsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }

		public string EmailFilter { get; set; }

		public string PhoneNoFilter { get; set; }



    }
}