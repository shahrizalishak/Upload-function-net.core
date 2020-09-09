using Abp.AutoMapper;
using eForm.Sessions.Dto;

namespace eForm.Models.Common
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput)),
     AutoMapTo(typeof(GetCurrentLoginInformationsOutput))]
    public class CurrentLoginInformationPersistanceModel
    {
        public UserLoginInfoPersistanceModel User { get; set; }

        public TenantLoginInfoPersistanceModel Tenant { get; set; }

        public ApplicationInfoPersistanceModel Application { get; set; }
    }
}