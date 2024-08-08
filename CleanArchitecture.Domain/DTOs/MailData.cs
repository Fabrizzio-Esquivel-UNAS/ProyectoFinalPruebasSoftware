using Newtonsoft.Json;

namespace CleanArchitecture.Domain.DTOs
{
    public class MailData
    {
        public required string EmailToId { get; set; }
        public required string EmailToName { get; set; }
        public required string EmailSubject { get; set; }
        public required string EmailBody { get; set; }
    }
}
