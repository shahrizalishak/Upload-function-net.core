using Abp.Application.Services.Dto;
using System;

namespace eForm.EFlight.Dtos
{
    public class GetAllJobTitlesForExcelInput
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }

		public string CodeFilter { get; set; }



    }
}