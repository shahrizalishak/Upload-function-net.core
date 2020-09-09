using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using eForm.DataExporting.Excel.EpPlus;
using eForm.EFlight.Dtos;
using eForm.Dto;
using eForm.Storage;

namespace eForm.EFlight.Exporting
{
    public class FlightInformationsExcelExporter : EpPlusExcelExporterBase, IFlightInformationsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public FlightInformationsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetFlightInformationForViewDto> flightInformations)
        {
            return CreateExcelPackage(
                "FlightInformations.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("FlightInformations"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("DestinationDeparture"),
                        L("DestinationArraival"),
                        L("Date"),
                        L("TImeDeparture"),
                        L("TimeArriaval"),
                        L("FlightId")
                        );

                    AddObjects(
                        sheet, 2, flightInformations,
                        _ => _.FlightInformation.DestinationDeparture,
                        _ => _.FlightInformation.DestinationArraival,
                        _ => _timeZoneConverter.Convert(_.FlightInformation.Date, _abpSession.TenantId, _abpSession.GetUserId()),
                        _ => _.FlightInformation.TImeDeparture,
                        _ => _.FlightInformation.TimeArriaval,
                        _ => _.FlightInformation.FlightId
                        );

					var dateColumn = sheet.Column(3);
                    dateColumn.Style.Numberformat.Format = "yyyy-mm-dd";
					dateColumn.AutoFit();
					

                });
        }
    }
}
