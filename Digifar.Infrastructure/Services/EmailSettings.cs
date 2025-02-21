namespace Digifar.Infrastructure.Services
{
    public class EmailSettings
    {
        public const string SectionName = "EmailSettings";
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ImapServer { get; set; }
        public int ImapPort { get; set; }
        public string? SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public int CodeExpirationMinutes { get; set; }
    }
}
