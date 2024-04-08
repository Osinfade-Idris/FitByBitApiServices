using FitByBitService.Entities.Models;
using FitByBitService.Entities.Responses;

namespace FitByBitService.Helpers;

public interface IGenerateOtpHandler
{
    public Task<VerificationOtpDto> GenerateOtp(User user);
}
