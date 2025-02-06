

namespace Digifar.Infrastructure.Services
{
    public class SmsSettings
    {
        public const string SectionName = "SmsSettings";
        public string? ApiKey { get; set; }
        public string? SenderId { get; set; }
    }
}
