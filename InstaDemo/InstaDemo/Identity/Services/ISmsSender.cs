using System.Threading.Tasks;

namespace InstaDemo.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
