using Digifar.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace Digifar.Infrastructure.Services
{
    public class MNotifySmsService(IOptions<SmsSettings> smsOptions, IHttpClientFactory httpClientFactory) : IMNotifySmsService
    {
        private readonly SmsSettings smsSettings = smsOptions.Value;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<string> SendSmsAsync(string recipient, string message)
        {
            var apiKey = smsSettings.ApiKey;
            var senderId = smsSettings.SenderId;

            var client = _httpClientFactory.CreateClient();
            var url = $"https://apps.mnotify.net/smsapi?key={apiKey}&to={recipient}&msg={Uri.EscapeDataString(message)}&sender_id={senderId}";

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return "Unable to send SMS";
            }

            return "SMS sent successfully";
        }
    }
}
