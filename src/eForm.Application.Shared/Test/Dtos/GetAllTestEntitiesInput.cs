using Abp.Application.Services.Dto;
using System;

namespace eForm.Test.Dtos
{
    public class GetAllTestEntitiesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }



    }
}