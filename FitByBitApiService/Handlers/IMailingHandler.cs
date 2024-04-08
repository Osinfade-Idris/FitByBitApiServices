using System.Net.Mail;
using FitByBitService.Entities.Responses;
using FitByBitService.Helpers;

namespace FitByBitService.Handlers;

public interface IMailingHandler
{
    Task<GenericResponse<SendMailDto>> SendEmailAsync(SendMailDto sendMailDto);
}
