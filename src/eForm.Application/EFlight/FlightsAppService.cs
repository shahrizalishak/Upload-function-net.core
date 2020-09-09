using eForm.EFlight;
using eForm.EFlight;
using eForm.EFlight;


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
	[AbpAuthorize(AppPermissions.Pages_Flights)]
    public class FlightsAppService : eFormAppServiceBase, IFlightsAppService
    {
		 private readonly IRepository<Flight> _flightRepository;
		 private readonly IFlightsExcelExporter _flightsExcelExporter;
		 private readonly IRepository<TravelAgent,int> _lookup_travelAgentRepository;
		 private readonly IRepository<Purpose,int> _lookup_purposeRepository;
		 private readonly IRepository<JobTitle,int> _lookup_jobTitleRepository;
		 

		  public FlightsAppService(IRepository<Flight> flightRepository, IFlightsExcelExporter flightsExcelExporter , IRepository<TravelAgent, int> lookup_travelAgentRepository, IRepository<Purpose, int> lookup_purposeRepository, IRepository<JobTitle, int> lookup_jobTitleRepository) 
		  {
			_flightRepository = flightRepository;
			_flightsExcelExporter = flightsExcelExporter;
			_lookup_travelAgentRepository = lookup_travelAgentRepository;
		_lookup_purposeRepository = lookup_purposeRepository;
		_lookup_jobTitleRepository = lookup_jobTitleRepository;
		
		  }

		 public async Task<PagedResultDto<GetFlightForViewDto>> GetAll(GetAllFlightsInput input)
         {
			
			var filteredFlights = _flightRepository.GetAll()
						.Include( e => e.TravelAgentFk)
						.Include( e => e.PurposeFk)
						.Include( e => e.JobTitleFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Detail.Contains(input.Filter) || e.Name.Contains(input.Filter) || e.NRIC.Contains(input.Filter) || e.StaffID.Contains(input.Filter) || e.Position.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.MembershipNo.Contains(input.Filter) || e.ValidationName.Contains(input.Filter) || e.ValidationPhoneNo.Contains(input.Filter) || e.ValidationPosition.Contains(input.Filter) || e.ApprovalName.Contains(input.Filter) || e.ApprovalPosition.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DetailFilter),  e => e.Detail == input.DetailFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NRICFilter),  e => e.NRIC == input.NRICFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.StaffIDFilter),  e => e.StaffID == input.StaffIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PositionFilter),  e => e.Position == input.PositionFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter),  e => e.Email == input.EmailFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter),  e => e.PhoneNo == input.PhoneNoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MembershipNoFilter),  e => e.MembershipNo == input.MembershipNoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValidationNameFilter),  e => e.ValidationName == input.ValidationNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValidationPhoneNoFilter),  e => e.ValidationPhoneNo == input.ValidationPhoneNoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValidationPositionFilter),  e => e.ValidationPosition == input.ValidationPositionFilter)
						.WhereIf(input.MinValidationDateFilter != null, e => e.ValidationDate >= input.MinValidationDateFilter)
						.WhereIf(input.MaxValidationDateFilter != null, e => e.ValidationDate <= input.MaxValidationDateFilter)
						.WhereIf(input.ValidationFilter > -1,  e => (input.ValidationFilter == 1 && e.Validation) || (input.ValidationFilter == 0 && !e.Validation) )
						.WhereIf(!string.IsNullOrWhiteSpace(input.ApprovalNameFilter),  e => e.ApprovalName == input.ApprovalNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ApprovalPositionFilter),  e => e.ApprovalPosition == input.ApprovalPositionFilter)
						.WhereIf(input.MinApprovalDateFilter != null, e => e.ApprovalDate >= input.MinApprovalDateFilter)
						.WhereIf(input.MaxApprovalDateFilter != null, e => e.ApprovalDate <= input.MaxApprovalDateFilter)
						.WhereIf(input.ApprovalFilter > -1,  e => (input.ApprovalFilter == 1 && e.Approval) || (input.ApprovalFilter == 0 && !e.Approval) )
						.WhereIf(!string.IsNullOrWhiteSpace(input.TravelAgentNameFilter), e => e.TravelAgentFk != null && e.TravelAgentFk.Name == input.TravelAgentNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PurposeNameFilter), e => e.PurposeFk != null && e.PurposeFk.Name == input.PurposeNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.JobTitleNameFilter), e => e.JobTitleFk != null && e.JobTitleFk.Name == input.JobTitleNameFilter);

			var pagedAndFilteredFlights = filteredFlights
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

			var flights = from o in pagedAndFilteredFlights
                         join o1 in _lookup_travelAgentRepository.GetAll() on o.TravelAgentId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_purposeRepository.GetAll() on o.PurposeId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_jobTitleRepository.GetAll() on o.JobTitleId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         
                         select new GetFlightForViewDto() {
							Flight = new FlightDto
							{
                                Detail = o.Detail,
                                Name = o.Name,
                                NRIC = o.NRIC,
                                StaffID = o.StaffID,
                                Position = o.Position,
                                Email = o.Email,
                                PhoneNo = o.PhoneNo,
                                MembershipNo = o.MembershipNo,
                                ValidationName = o.ValidationName,
                                ValidationPhoneNo = o.ValidationPhoneNo,
                                ValidationPosition = o.ValidationPosition,
                                ValidationDate = o.ValidationDate,
                                Validation = o.Validation,
                                ApprovalName = o.ApprovalName,
                                ApprovalPosition = o.ApprovalPosition,
                                ApprovalDate = o.ApprovalDate,
                                Approval = o.Approval,
                                Id = o.Id
							},
                         	TravelAgentName = s1 == null ? "" : s1.Name.ToString(),
                         	PurposeName = s2 == null ? "" : s2.Name.ToString(),
                         	JobTitleName = s3 == null ? "" : s3.Name.ToString()
						};

            var totalCount = await filteredFlights.CountAsync();

            return new PagedResultDto<GetFlightForViewDto>(
                totalCount,
                await flights.ToListAsync()
            );
         }
		 
		 public async Task<GetFlightForViewDto> GetFlightForView(int id)
         {
            var flight = await _flightRepository.GetAsync(id);

            var output = new GetFlightForViewDto { Flight = ObjectMapper.Map<FlightDto>(flight) };

		    if (output.Flight.TravelAgentId != null)
            {
                var _lookupTravelAgent = await _lookup_travelAgentRepository.FirstOrDefaultAsync((int)output.Flight.TravelAgentId);
                output.TravelAgentName = _lookupTravelAgent.Name.ToString();
            }

		    if (output.Flight.PurposeId != null)
            {
                var _lookupPurpose = await _lookup_purposeRepository.FirstOrDefaultAsync((int)output.Flight.PurposeId);
                output.PurposeName = _lookupPurpose.Name.ToString();
            }

		    if (output.Flight.JobTitleId != null)
            {
                var _lookupJobTitle = await _lookup_jobTitleRepository.FirstOrDefaultAsync((int)output.Flight.JobTitleId);
                output.JobTitleName = _lookupJobTitle.Name.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Flights_Edit)]
		 public async Task<GetFlightForEditOutput> GetFlightForEdit(EntityDto input)
         {
            var flight = await _flightRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetFlightForEditOutput {Flight = ObjectMapper.Map<CreateOrEditFlightDto>(flight)};

		    if (output.Flight.TravelAgentId != null)
            {
                var _lookupTravelAgent = await _lookup_travelAgentRepository.FirstOrDefaultAsync((int)output.Flight.TravelAgentId);
                output.TravelAgentName = _lookupTravelAgent.Name.ToString();
            }

		    if (output.Flight.PurposeId != null)
            {
                var _lookupPurpose = await _lookup_purposeRepository.FirstOrDefaultAsync((int)output.Flight.PurposeId);
                output.PurposeName = _lookupPurpose.Name.ToString();
            }

		    if (output.Flight.JobTitleId != null)
            {
                var _lookupJobTitle = await _lookup_jobTitleRepository.FirstOrDefaultAsync((int)output.Flight.JobTitleId);
                output.JobTitleName = _lookupJobTitle.Name.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditFlightDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Flights_Create)]
		 protected virtual async Task Create(CreateOrEditFlightDto input)
         {
            var flight = ObjectMapper.Map<Flight>(input);

			
			if (AbpSession.TenantId != null)
			{
				flight.TenantId = (int?) AbpSession.TenantId;
			}
		

            await _flightRepository.InsertAsync(flight);
         }

		 [AbpAuthorize(AppPermissions.Pages_Flights_Edit)]
		 protected virtual async Task Update(CreateOrEditFlightDto input)
         {
            var flight = await _flightRepository.FirstOrDefaultAsync((int)input.Id);
             ObjectMapper.Map(input, flight);
         }

		 [AbpAuthorize(AppPermissions.Pages_Flights_Delete)]
         public async Task Delete(EntityDto input)
         {
            await _flightRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetFlightsToExcel(GetAllFlightsForExcelInput input)
         {
			
			var filteredFlights = _flightRepository.GetAll()
						.Include( e => e.TravelAgentFk)
						.Include( e => e.PurposeFk)
						.Include( e => e.JobTitleFk)
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.Detail.Contains(input.Filter) || e.Name.Contains(input.Filter) || e.NRIC.Contains(input.Filter) || e.StaffID.Contains(input.Filter) || e.Position.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.PhoneNo.Contains(input.Filter) || e.MembershipNo.Contains(input.Filter) || e.ValidationName.Contains(input.Filter) || e.ValidationPhoneNo.Contains(input.Filter) || e.ValidationPosition.Contains(input.Filter) || e.ApprovalName.Contains(input.Filter) || e.ApprovalPosition.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.DetailFilter),  e => e.Detail == input.DetailFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NameFilter),  e => e.Name == input.NameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.NRICFilter),  e => e.NRIC == input.NRICFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.StaffIDFilter),  e => e.StaffID == input.StaffIDFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PositionFilter),  e => e.Position == input.PositionFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter),  e => e.Email == input.EmailFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter),  e => e.PhoneNo == input.PhoneNoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.MembershipNoFilter),  e => e.MembershipNo == input.MembershipNoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValidationNameFilter),  e => e.ValidationName == input.ValidationNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValidationPhoneNoFilter),  e => e.ValidationPhoneNo == input.ValidationPhoneNoFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ValidationPositionFilter),  e => e.ValidationPosition == input.ValidationPositionFilter)
						.WhereIf(input.MinValidationDateFilter != null, e => e.ValidationDate >= input.MinValidationDateFilter)
						.WhereIf(input.MaxValidationDateFilter != null, e => e.ValidationDate <= input.MaxValidationDateFilter)
						.WhereIf(input.ValidationFilter > -1,  e => (input.ValidationFilter == 1 && e.Validation) || (input.ValidationFilter == 0 && !e.Validation) )
						.WhereIf(!string.IsNullOrWhiteSpace(input.ApprovalNameFilter),  e => e.ApprovalName == input.ApprovalNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ApprovalPositionFilter),  e => e.ApprovalPosition == input.ApprovalPositionFilter)
						.WhereIf(input.MinApprovalDateFilter != null, e => e.ApprovalDate >= input.MinApprovalDateFilter)
						.WhereIf(input.MaxApprovalDateFilter != null, e => e.ApprovalDate <= input.MaxApprovalDateFilter)
						.WhereIf(input.ApprovalFilter > -1,  e => (input.ApprovalFilter == 1 && e.Approval) || (input.ApprovalFilter == 0 && !e.Approval) )
						.WhereIf(!string.IsNullOrWhiteSpace(input.TravelAgentNameFilter), e => e.TravelAgentFk != null && e.TravelAgentFk.Name == input.TravelAgentNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.PurposeNameFilter), e => e.PurposeFk != null && e.PurposeFk.Name == input.PurposeNameFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.JobTitleNameFilter), e => e.JobTitleFk != null && e.JobTitleFk.Name == input.JobTitleNameFilter);

			var query = (from o in filteredFlights
                         join o1 in _lookup_travelAgentRepository.GetAll() on o.TravelAgentId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         join o2 in _lookup_purposeRepository.GetAll() on o.PurposeId equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()
                         
                         join o3 in _lookup_jobTitleRepository.GetAll() on o.JobTitleId equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()
                         
                         select new GetFlightForViewDto() { 
							Flight = new FlightDto
							{
                                Detail = o.Detail,
                                Name = o.Name,
                                NRIC = o.NRIC,
                                StaffID = o.StaffID,
                                Position = o.Position,
                                Email = o.Email,
                                PhoneNo = o.PhoneNo,
                                MembershipNo = o.MembershipNo,
                                ValidationName = o.ValidationName,
                                ValidationPhoneNo = o.ValidationPhoneNo,
                                ValidationPosition = o.ValidationPosition,
                                ValidationDate = o.ValidationDate,
                                Validation = o.Validation,
                                ApprovalName = o.ApprovalName,
                                ApprovalPosition = o.ApprovalPosition,
                                ApprovalDate = o.ApprovalDate,
                                Approval = o.Approval,
                                Id = o.Id
							},
                         	TravelAgentName = s1 == null ? "" : s1.Name.ToString(),
                         	PurposeName = s2 == null ? "" : s2.Name.ToString(),
                         	JobTitleName = s3 == null ? "" : s3.Name.ToString()
						 });


            var flightListDtos = await query.ToListAsync();

            return _flightsExcelExporter.ExportToFile(flightListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_Flights)]
         public async Task<PagedResultDto<FlightTravelAgentLookupTableDto>> GetAllTravelAgentForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_travelAgentRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> (e.Name != null ? e.Name.ToString():"").Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var travelAgentList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<FlightTravelAgentLookupTableDto>();
			foreach(var travelAgent in travelAgentList){
				lookupTableDtoList.Add(new FlightTravelAgentLookupTableDto
				{
					Id = travelAgent.Id,
					DisplayName = travelAgent.Name?.ToString()
				});
			}

            return new PagedResultDto<FlightTravelAgentLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_Flights)]
         public async Task<PagedResultDto<FlightPurposeLookupTableDto>> GetAllPurposeForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_purposeRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> (e.Name != null ? e.Name.ToString():"").Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var purposeList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<FlightPurposeLookupTableDto>();
			foreach(var purpose in purposeList){
				lookupTableDtoList.Add(new FlightPurposeLookupTableDto
				{
					Id = purpose.Id,
					DisplayName = purpose.Name?.ToString()
				});
			}

            return new PagedResultDto<FlightPurposeLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }

		[AbpAuthorize(AppPermissions.Pages_Flights)]
         public async Task<PagedResultDto<FlightJobTitleLookupTableDto>> GetAllJobTitleForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _lookup_jobTitleRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> (e.Name != null ? e.Name.ToString():"").Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var jobTitleList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<FlightJobTitleLookupTableDto>();
			foreach(var jobTitle in jobTitleList){
				lookupTableDtoList.Add(new FlightJobTitleLookupTableDto
				{
					Id = jobTitle.Id,
					DisplayName = jobTitle.Name?.ToString()
				});
			}

            return new PagedResultDto<FlightJobTitleLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}