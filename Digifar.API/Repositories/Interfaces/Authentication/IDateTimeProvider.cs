namespace Digifar.API.Repositories.Interfaces.Authentication
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
