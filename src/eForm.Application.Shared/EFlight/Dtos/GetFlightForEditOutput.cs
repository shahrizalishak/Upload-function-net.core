using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace eForm.EFlight.Dtos
{
    public class GetFlightForEditOutput
    {
		public CreateOrEditFlightDto Flight { get; set; }

		public string TravelAgentName { get; set;}

		public string PurposeName { get; set;}

		public string JobTitleName { get; set;}


    }
}