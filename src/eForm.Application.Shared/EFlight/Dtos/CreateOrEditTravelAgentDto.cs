
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace eForm.EFlight.Dtos
{
    public class CreateOrEditTravelAgentDto : EntityDto<int?>
    {

		public string Name { get; set; }
		
		
		public string Email { get; set; }
		
		
		public string PhoneNo { get; set; }
		
		

    }
}