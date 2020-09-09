using Abp.Application.Services.Dto;
using System;

namespace eForm.EFlight.Dtos
{
    public class GetAllFlightInformationsForExcelInput
    {
		public string Filter { get; set; }

		public string DestinationDepartureFilter { get; set; }

		public string DestinationArraivalFilter { get; set; }

		public DateTime? MaxDateFilter { get; set; }
		public DateTime? MinDateFilter { get; set; }

		public string TImeDepartureFilter { get; set; }

		public string TimeArriavalFilter { get; set; }

		public int? MaxFlightIdFilter { get; set; }
		public int? MinFlightIdFilter { get; set; }



    }
}