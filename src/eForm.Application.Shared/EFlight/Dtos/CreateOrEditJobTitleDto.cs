
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace eForm.EFlight.Dtos
{
    public class CreateOrEditJobTitleDto : EntityDto<int?>
    {

		[Required]
		public string Name { get; set; }
		
		
		[Required]
		public string Code { get; set; }
		
		

    }
}