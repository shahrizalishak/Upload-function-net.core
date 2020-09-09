using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using eForm.DataExporting.Excel.EpPlus;
using eForm.EFlight.Dtos;
using eForm.Dto;
using eForm.Storage;

namespace eForm.EFlight.Exporting
{
    public class TravelAgentsExcelExporter : EpPlusExcelExporterBase, ITravelAgentsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TravelAgentsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTravelAgentForViewDto> travelAgents)
        {
            return CreateExcelPackage(
                "TravelAgents.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("TravelAgents"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Email"),
                        L("PhoneNo")
                        );

                    AddObjects(
                        sheet, 2, travelAgents,
                        _ => _.TravelAgent.Name,
                        _ => _.TravelAgent.Email,
                        _ => _.TravelAgent.PhoneNo
                        );

					

                });
        }
    }
}
