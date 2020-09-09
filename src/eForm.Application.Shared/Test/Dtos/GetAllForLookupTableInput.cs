using Abp.Application.Services.Dto;

namespace eForm.Test.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}