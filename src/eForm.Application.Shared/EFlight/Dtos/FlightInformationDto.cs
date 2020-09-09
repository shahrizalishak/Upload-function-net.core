
using System;
using Abp.Application.Services.Dto;

namespace eForm.EFlight.Dtos
{
    public class FlightInformationDto : EntityDto
    {
		public string DestinationDeparture { get; set; }

		public string DestinationArraival { get; set; }

		public DateTime Date { get; set; }

		public string TImeDeparture { get; set; }

		public string TimeArriaval { get; set; }

		public int FlightId { get; set; }



    }
}