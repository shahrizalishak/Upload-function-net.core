
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace eForm.EFlight.Dtos
{
    public class CreateOrEditFlightInformationDto : EntityDto<int?>
    {

		[Required]
		public string DestinationDeparture { get; set; }
		
		
		public string DestinationArraival { get; set; }
		
		
		public DateTime Date { get; set; }
		
		
		public string TImeDeparture { get; set; }
		
		
		public string TimeArriaval { get; set; }
		
		
		public int FlightId { get; set; }
		
		

    }
}