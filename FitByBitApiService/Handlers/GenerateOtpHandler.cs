using AutoMapper;
using FitByBitService.Data;
using FitByBitService.Entities.Models;
using FitByBitService.Entities.Responses;
using Microsoft.EntityFrameworkCore;

namespace FitByBitService.Handlers;

public class GenerateOtpHandler : IGenerateOtpHandler
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly string _otpSecretKey;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<GenerateOtpHandler> _logger;
    private readonly ApplicationDbContext _dbContext;
    private readonly DateTime _dateTime;

    public GenerateOtpHandler(IConfiguration configuration, IMapper mapper, IServiceProvider serviceProvider,
        ILogger<GenerateOtpHandler> logger, ApplicationDbContext dbContext)
    {
        _configuration = configuration;
        _mapper = mapper;
        _otpSecretKey = configuration["OtpEncryptionSettings:SecretKey"]!;
        _serviceProvider = serviceProvider;
        _logger = logger;
        _dbContext = dbContext;
        _dateTime = DateTime.Now;
    }

    public async Task<VerificationOtpDto> GenerateOtp(User user)
    {
        _logger.LogInformation($"\n---------- Generating valid otp | {_dateTime} ------------\n".ToUpper());

        /*using var scope = _serviceProvider.CreateScope();
        var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();*/

        int code;
        do
        {
            code = new Random().Next(000000, 999999);
        }
        while (await _dbContext.VerificationOtps.AnyAsync(otp => otp.Code == code.ToString()));

        var encryptedOtp = CommonEncryptionHandler.Encrypt(Convert.ToString(code), _otpSecretKey);
        int expiry = int.Parse(_configuration["JwtSettings:Expires"]);

        var otpObjectDto = new VerificationOtpDto()
        {
            User = user,
            Code = encryptedOtp,
            Expiry = DateTimeOffset.Now.AddMinutes(expiry)
        };

        var otpDomain = _mapper.Map<VerificationOtp>(otpObjectDto);
        otpDomain.User = null;
        otpDomain.UserId = user.Id;

        _logger.LogInformation($"\n--------- Saving otp to database | {_dateTime} -------------\n".ToUpper());

        await _dbContext.VerificationOtps.AddAsync(otpDomain);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"\n---------- otp generation completed | {_dateTime} ------------\n".ToUpper());

        otpObjectDto.Code = code.ToString();

        return otpObjectDto;
    }
}
