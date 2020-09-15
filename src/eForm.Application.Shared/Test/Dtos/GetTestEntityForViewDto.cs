using System.Collections.Generic;

namespace eForm.Test.Dtos
{
    public class GetTestEntityForViewDto
    {
		public TestEntityDto TestEntity { get; set; }

        public ICollection<TempUploadDto> TempUpload { get; set; }

        public IList<string> TempUploadListID { get; set; }

    }
}