using Digifar.Application.Common.Interfaces.Services;

namespace Digifar.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
