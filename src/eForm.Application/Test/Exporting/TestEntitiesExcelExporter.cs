using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using eForm.DataExporting.Excel.EpPlus;
using eForm.Test.Dtos;
using eForm.Dto;
using eForm.Storage;

namespace eForm.Test.Exporting
{
    public class TestEntitiesExcelExporter : EpPlusExcelExporterBase, ITestEntitiesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TestEntitiesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTestEntityForViewDto> testEntities)
        {
            return CreateExcelPackage(
                "TestEntities.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("TestEntities"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name")
                        );

                    AddObjects(
                        sheet, 2, testEntities,
                        _ => _.TestEntity.Name
                        );

					

                });
        }
    }
}
