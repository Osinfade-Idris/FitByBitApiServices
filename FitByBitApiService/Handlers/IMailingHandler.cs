using System.Net.Mail;
using FitByBitApiService.Entities.Responses;
using FitByBitApiService.Helpers;

namespace FitByBitApiService.Handlers;

public interface IMailingHandler
{
    Task<GenericResponse<SendMailDto>> SendEmailAsync(SendMailDto sendMailDto);
}
