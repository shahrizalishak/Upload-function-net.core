using System.Threading.Tasks;
using Abp.Dependency;

namespace eForm.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}