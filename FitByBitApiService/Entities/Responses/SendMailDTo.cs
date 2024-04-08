using System.ComponentModel.DataAnnotations;

namespace FitByBitService.Entities.Responses
{
    public class SendMailDto
    {
        [Required(ErrorMessage = "Receiver Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string ReceiverEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Subject is required.")]
        public string EmailSubject { get; set; } = string.Empty;

        [DataType(DataType.Html)]
        public string HtmlEmailMessage { get; set; } = string.Empty;
        public List<IFormFile>? Attachments { get; set; }
    }
}
