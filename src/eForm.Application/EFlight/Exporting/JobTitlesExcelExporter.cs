using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using eForm.DataExporting.Excel.EpPlus;
using eForm.EFlight.Dtos;
using eForm.Dto;
using eForm.Storage;

namespace eForm.EFlight.Exporting
{
    public class JobTitlesExcelExporter : EpPlusExcelExporterBase, IJobTitlesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public JobTitlesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetJobTitleForViewDto> jobTitles)
        {
            return CreateExcelPackage(
                "JobTitles.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("JobTitles"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Code")
                        );

                    AddObjects(
                        sheet, 2, jobTitles,
                        _ => _.JobTitle.Name,
                        _ => _.JobTitle.Code
                        );

					

                });
        }
    }
}
