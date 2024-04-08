using FitByBitService.Entities.Models;
using FitByBitService.Entities.Responses;

namespace FitByBitService.Handlers;

public interface IGenerateOtpHandler
{
    public Task<VerificationOtpDto> GenerateOtp(User user);
}
