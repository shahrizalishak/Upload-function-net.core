using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace eForm.EFlight
{
	[Table("FlightInformations")]
    public class FlightInformation : Entity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		[Required]
		public virtual string DestinationDeparture { get; set; }
		
		public virtual string DestinationArraival { get; set; }
		
		public virtual DateTime Date { get; set; }
		
		public virtual string TImeDeparture { get; set; }
		
		public virtual string TimeArriaval { get; set; }
		
		public virtual int FlightId { get; set; }
		

    }
}