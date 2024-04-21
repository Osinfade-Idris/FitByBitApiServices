using MediatR;
namespace FitByBitApiService.Events;

public class SendMailNotificationEvent : INotification
{
    public string ReceiverEmail { get; set; } = string.Empty;
    public string EmailSubject { get; set; } = string.Empty;
    public string? HtmlEmailMessage { get; set; } = string.Empty;
    public List<IFormFile>? Attachments { get; set; }
}
