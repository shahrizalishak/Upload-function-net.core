using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using eForm.MultiTenancy.Accounting.Dto;

namespace eForm.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
