using Abp.Application.Services.Dto;
using System;

namespace eForm.EFlight.Dtos
{
    public class GetAllPurposesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }



    }
}