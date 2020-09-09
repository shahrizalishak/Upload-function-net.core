using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace eForm.EFlight.Dtos
{
    public class GetFlightInformationForEditOutput
    {
		public CreateOrEditFlightInformationDto FlightInformation { get; set; }


    }
}