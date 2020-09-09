
using System;
using Abp.Application.Services.Dto;

namespace eForm.EFlight.Dtos
{
    public class TravelAgentDto : EntityDto
    {
		public string Name { get; set; }

		public string Email { get; set; }

		public string PhoneNo { get; set; }



    }
}