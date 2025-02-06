
namespace Digifar.Application.Common.Interfaces.Services
{
    public interface IMNotifySmsService
    {
        Task<string> SendSmsAsync(string recipient, string message);
    }
}
