using FitByBitApiService.Entities.Models;
using FitByBitApiService.Entities.Responses;

namespace FitByBitApiService.Helpers;

public interface IGenerateOtpHandler
{
    public Task<VerificationOtpDto> GenerateOtp(User user);
}
