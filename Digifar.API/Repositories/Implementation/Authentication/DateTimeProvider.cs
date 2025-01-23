using Digifar.API.Repositories.Interfaces.Authentication;

namespace Digifar.API.Repositories.Implementation.Authentication
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
