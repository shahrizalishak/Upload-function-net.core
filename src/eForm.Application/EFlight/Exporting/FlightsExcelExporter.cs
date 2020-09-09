using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using eForm.DataExporting.Excel.EpPlus;
using eForm.EFlight.Dtos;
using eForm.Dto;
using eForm.Storage;

namespace eForm.EFlight.Exporting
{
    public class FlightsExcelExporter : EpPlusExcelExporterBase, IFlightsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public FlightsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetFlightForViewDto> flights)
        {
            return CreateExcelPackage(
                "Flights.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Flights"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Detail"),
                        L("Name"),
                        L("NRIC"),
                        L("StaffID"),
                        L("Position"),
                        L("Email"),
                        L("PhoneNo"),
                        L("MembershipNo"),
                        L("ValidationName"),
                        L("ValidationPhoneNo"),
                        L("ValidationPosition"),
                        L("ValidationDate"),
                        L("Validation"),
                        L("ApprovalName"),
                        L("ApprovalPosition"),
                        L("ApprovalDate"),
                        L("Approval"),
                        (L("TravelAgent")) + L("Name"),
                        (L("Purpose")) + L("Name"),
                        (L("JobTitle")) + L("Name")
                        );

                    AddObjects(
                        sheet, 2, flights,
                        _ => _.Flight.Detail,
                        _ => _.Flight.Name,
                        _ => _.Flight.NRIC,
                        _ => _.Flight.StaffID,
                        _ => _.Flight.Position,
                        _ => _.Flight.Email,
                        _ => _.Flight.PhoneNo,
                        _ => _.Flight.MembershipNo,
                        _ => _.Flight.ValidationName,
                        _ => _.Flight.ValidationPhoneNo,
                        _ => _.Flight.ValidationPosition,
                        _ => _timeZoneConverter.Convert(_.Flight.ValidationDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Flight.Validation,
                        _ => _.Flight.ApprovalName,
                        _ => _.Flight.ApprovalPosition,
                        _ => _timeZoneConverter.Convert(_.Flight.ApprovalDate, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.Flight.Approval,
                        _ => _.TravelAgentName,
                        _ => _.PurposeName,
                        _ => _.JobTitleName
                        );

					var validationDateColumn = sheet.Column(12);
                    validationDateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					validationDateColumn.AutoFit();
					var approvalDateColumn = sheet.Column(16);
                    approvalDateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					approvalDateColumn.AutoFit();
					

                });
        }
    }
}
