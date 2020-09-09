using System.Threading.Tasks;

namespace eForm.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}