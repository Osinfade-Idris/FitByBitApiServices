using System.Net;
using FitByBitApiService.Configuration;
using FitByBitApiService.Entities.Responses;
using FitByBitApiService.Helpers;
using FitByBitApiService.Exceptions;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;


namespace FitByBitApiService.Handlers;

public class MailingHandler : IMailingHandler
{
    private readonly ILogger<MailingHandler> _logger;
    private readonly SmtpConfiguration _smtpConfig;
    private readonly DateTime _dateTime;

    public MailingHandler(ILogger<MailingHandler> logger, IOptions<SmtpConfiguration> smtpConfig)
    {
        _logger = logger;
        _smtpConfig = smtpConfig.Value;
        _dateTime = DateTime.Now;
    }

    public async Task<GenericResponse<SendMailDto>> SendEmailAsync(SendMailDto sendMailDto)
    {
        _logger.LogInformation($"\n----- preparing to send mail to {sendMailDto.ReceiverEmail} | {_dateTime} -----\n".ToUpper());

        var mailContext = new MimeMessage()
        {
            From = { MailboxAddress.Parse(_smtpConfig.FromEmail) },
            To = { MailboxAddress.Parse(sendMailDto.ReceiverEmail) },
            Subject = sendMailDto.EmailSubject,
            Body = new TextPart(TextFormat.Html)
            {
                Text = sendMailDto.HtmlEmailMessage
            },
        };

        var smtp = new SmtpClient();
        await smtp.ConnectAsync(_smtpConfig.SmtpHost, _smtpConfig.SmtpPort, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_smtpConfig.SmtpUser, _smtpConfig.SmtpPassword);
        try
        {
            await smtp.SendAsync(mailContext);
            await smtp.DisconnectAsync(true);
            _logger.LogInformation($"\n----- Mail successfully sent to {sendMailDto.ReceiverEmail} | {_dateTime} -----\n".ToUpper());
            return new GenericResponse<SendMailDto>
            {
                Data = null,
                Message = $"Mail successfully sent to {sendMailDto.ReceiverEmail}",
                Success = true,
                StatusCode = HttpStatusCode.Accepted
            };
        }
        catch (Exception exception)
        {
            _logger.LogInformation($"\n----- {exception.Message}: Could not send mail to {sendMailDto.ReceiverEmail} | {_dateTime} -----\n".ToUpper());
            throw new FitByBitBadRequestException($"Could not send mail to {sendMailDto.ReceiverEmail}", HttpStatusCode.BadRequest.ToString());
        }
    }
}
