using System.Collections.Generic;
using Abp.Application.Services.Dto;
using eForm.Editions.Dto;

namespace eForm.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}