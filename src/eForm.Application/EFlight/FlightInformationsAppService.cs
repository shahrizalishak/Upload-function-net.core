

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using eForm.EFlight.Exporting;
using eForm.EFlight.Dtos;
using eForm.Dto;
using Abp.Application.Services.Dto;
using eForm.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace eForm.EFlight
{
	[AbpAuthorize(AppPermissions.Pages_FlightInformations)]
    public class FlightInformationsAppService : eFormAppServiceBase, IFlightInformationsAppService
    {
		 private readonly IRepository<FlightInformation> _flightInformationRepository;
		 private readonly IFlightInformationsExcelExporter _flightInformationsExcelExporter;
		 

		  public FlightInformationsAppService(IRepository<FlightInformation> flightInformationRepository, IFlightInformationsExcelExporter flightInformationsExcelExporter ) 
		  {
			_flightInformationRepository = flightInformationRepository;
			_flightInformationsExcelExporter = flightInformationsExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetFlightInformationForViewDto>> GetAll(GetAllFlightInformationsInput input)
         {
			
			var filteredFlightInformations = _flightInformationRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.DestinationDeparture.Contains(input.Filter) || e.DestinationArraival.Contains(input.Filter) || e.TImeDeparture.Contains(input.Filter) || e.TimeArriaval.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DestinationDepartureFilter),  e => e.DestinationDeparture == input.DestinationDepartureFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DestinationArraivalFilter),  e => e.DestinationArraival == input.DestinationArraivalFilter)
						.WhereIf(input.MinDateFilter != null, e => e.Date >= input.MinDateFilter)
						.WhereIf(input.MaxDateFilter != null, e => e.Date <= input.MaxDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TImeDepartureFilter),  e => e.TImeDeparture == input.TImeDepartureFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TimeArriavalFilter),  e => e.TimeArriaval == input.TimeArriavalFilter)
						.WhereIf(input.MinFlightIdFilter != null, e => e.FlightId >= input.MinFlightIdFilter)
						.WhereIf(input.MaxFlightIdFilter != null, e => e.FlightId <= input.MaxFlightIdFilter);

			var pagedAndFilteredFlightInformations = filteredFlightInformations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var flightInformations = from o in pagedAndFilteredFlightInformations
                         select new GetFlightInformationForViewDto() {
							FlightInformation = new FlightInformationDto
							{
                                DestinationDeparture = o.DestinationDeparture,
                                DestinationArraival = o.DestinationArraival,
                                Date = o.Date,
                                TImeDeparture = o.TImeDeparture,
                                TimeArriaval = o.TimeArriaval,
                                FlightId = o.FlightId,
                                Id = o.Id
							}
						};

            var totalCount = await filteredFlightInformations.CountAsync();

            return new PagedResultDto<GetFlightInformationForViewDto>(
                totalCount,
                await flightInformations.ToListAsync()
            );
         }
		 
		 public async Task<GetFlightInformationForViewDto> GetFlightInformationForView(int id)
         {
            var flightInformation = await _flightInformationRepository.GetAsync(id);

            var output = new GetFlightInformationForViewDto { FlightInformation = ObjectMapper.Map<FlightInformationDto>(flightInformation) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_FlightInformations_Edit)]
		 public async Task<GetFlightInformationForEditOutput> GetFlightInformationForEdit(EntityDto input)
         {
            var flightInformation = await _flightInformationRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetFlightInformationForEditOutput {FlightInformation = ObjectMapper.Map<CreateOrEditFlightInformationDto>(flightInformation)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditFlightInformationDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_FlightInformations_Create)]
		 protected virtual async Task Create(CreateOrEditFlightInformationDto input)
         {
            var flightInformation = ObjectMapper.Map<FlightInformation>(input);

			
			if (AbpSession.TenantId != null)
			{
				flightInformation.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _flightInformationRepository.InsertAsync(flightInformation);
         }

		 [AbpAuthorize(AppPermissions.Pages_FlightInformations_Edit)]
		 protected virtual async Task Update(CreateOrEditFlightInformationDto input)
         {
            var flightInformation = await _flightInformationRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, flightInformation);
         }

		 [AbpAuthorize(AppPermissions.Pages_FlightInformations_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _flightInformationRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetFlightInformationsToExcel(GetAllFlightInformationsForExcelInput input)
         {
			
			var filteredFlightInformations = _flightInformationRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.DestinationDeparture.Contains(input.Filter) || e.DestinationArraival.Contains(input.Filter) || e.TImeDeparture.Contains(input.Filter) || e.TimeArriaval.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DestinationDepartureFilter),  e => e.DestinationDeparture == input.DestinationDepartureFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.DestinationArraivalFilter),  e => e.DestinationArraival == input.DestinationArraivalFilter)
						.WhereIf(input.MinDateFilter != null, e => e.Date >= input.MinDateFilter)
						.WhereIf(input.MaxDateFilter != null, e => e.Date <= input.MaxDateFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TImeDepartureFilter),  e => e.TImeDeparture == input.TImeDepartureFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TimeArriavalFilter),  e => e.TimeArriaval == input.TimeArriavalFilter)
						.WhereIf(input.MinFlightIdFilter != null, e => e.FlightId >= input.MinFlightIdFilter)
						.WhereIf(input.MaxFlightIdFilter != null, e => e.FlightId <= input.MaxFlightIdFilter);

			var query = (from o in filteredFlightInformations
                         select new GetFlightInformationForViewDto() { 
							FlightInformation = new FlightInformationDto
							{
                                DestinationDeparture = o.DestinationDeparture,
                                DestinationArraival = o.DestinationArraival,
                                Date = o.Date,
                                TImeDeparture = o.TImeDeparture,
                                TimeArriaval = o.TimeArriaval,
                                FlightId = o.FlightId,
                                Id = o.Id
							}
						 });


            var flightInformationListDtos = await query.ToListAsync();

            return _flightInformationsExcelExporter.ExportToFile(flightInformationListDtos);
         }


    }
}