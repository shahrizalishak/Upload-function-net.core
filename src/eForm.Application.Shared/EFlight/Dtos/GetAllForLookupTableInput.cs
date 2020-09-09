using Abp.Application.Services.Dto;

namespace eForm.EFlight.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}