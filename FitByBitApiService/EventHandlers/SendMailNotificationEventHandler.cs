using System.Net;
using System.Net.Mime;
using System.Text;
using AutoMapper;
using FitByBitService.Entities.Responses;
using FitByBitService.Events;
using FitByBitService.Exceptions;
using FitByBitService.Handlers;
using MediatR;
using Newtonsoft.Json;

namespace FitByBitService.EventHandlers;

public class SendMailNotificationEventHandler : INotificationHandler<SendMailNotificationEvent>
{
    private readonly ILogger<SendMailNotificationEventHandler> _logger;
    private readonly IMapper _imapper;
    private readonly IMailingHandler _mailingHandler;
    private readonly DateTime _dateTime;

    public SendMailNotificationEventHandler(ILogger<SendMailNotificationEventHandler> logger, IMapper imapper,
        IMailingHandler mailingHandler)
    {
        _logger = logger;
        _imapper = imapper;
        _mailingHandler = mailingHandler;
        _dateTime = DateTime.Now;
    }

    public async Task Handle(SendMailNotificationEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"\n------- sending mail to | {notification.ReceiverEmail} | {_dateTime} ------\n".ToUpper());

        try
        {
            var sendMailDto = new SendMailDto()
            {
                ReceiverEmail = notification.ReceiverEmail,
                EmailSubject = notification.EmailSubject,
                HtmlEmailMessage = notification.HtmlEmailMessage!,
                Attachments = notification.Attachments
            };

            var result = await _mailingHandler.SendEmailAsync(sendMailDto);
            _logger.LogInformation($"\n-------- response: | {result} | {_dateTime} --------\n");
        }
        catch (Exception exception)
        {
            _logger.LogInformation($"\n--------- {exception}: Unable to send mail | {_dateTime}  ------------\n".ToUpper());

            string message = "Could not send otp, kindly head on to login to request for a new activation otp.";
            throw new FitByBitServiceUnavailableException($"{message}", HttpStatusCode.InternalServerError.ToString());
        }

        _logger.LogInformation($"\n---------- mail successfully sent to | {notification.ReceiverEmail} | {_dateTime} -----------\n".ToUpper());
    }
}
