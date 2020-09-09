
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace eForm.EFlight.Dtos
{
    public class CreateOrEditPurposeDto : EntityDto<int?>
    {

		[Required]
		public string Name { get; set; }
		
		

    }
}