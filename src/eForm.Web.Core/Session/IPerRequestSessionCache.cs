using System.Threading.Tasks;
using eForm.Sessions.Dto;

namespace eForm.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
