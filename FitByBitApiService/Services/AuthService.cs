using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FitByBitService.Data;
using FitByBitService.Entities.Models;
using FitByBitService.Entities.Responses;
using FitByBitService.Entities.Responses.UserResponse;
using FitByBitService.Events;
using FitByBitService.Exceptions;
using FitByBitService.Helpers;
using FitByBitService.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using CommonEncryptionHandler = FitByBitService.Handlers.CommonEncryptionHandler;
using IGenerateOtpHandler = FitByBitService.Handlers.IGenerateOtpHandler;


namespace FitByBitService.Services;

public class AuthService : IAuthRepository
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<AuthService> _logger;
    private readonly IGenerateOtpHandler _generateOtpHandler;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly DateTime _dateTime;
    private readonly string _otpSecretKey;

    public AuthService(IMapper mapper, UserManager<User> userManager, ILogger<AuthService> logger, IMediator mediator,
        IGenerateOtpHandler generateOtpHandler, IConfiguration configuration, SignInManager<User> signInManager,
        ApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _userManager = userManager;
        _logger = logger;
        _generateOtpHandler = generateOtpHandler;
        _mediator = mediator;
        _configuration = configuration;
        _otpSecretKey = _configuration["OtpEncryptionSettings:SecretKey"]!;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _dateTime = DateTime.Now;
    }

    public async Task<GenericResponse<CreateJwtTokenDto>> CreateUserAsync(CreateUserDto createUserDto)
    {
        _logger.LogInformation($"\n--------- starting account creation | {_dateTime} -----------\n".ToUpper());

        var user = await _userManager.FindByEmailAsync(createUserDto.Email);

        _logger.LogInformation($"\n--------- verifying if profile exist | {_dateTime} -----------\n".ToUpper());

        if (user != null)
        {
            _logger.LogInformation($"\n------ email with {createUserDto.Email} already exists| {_dateTime} -------\n");
            throw new FitByBitBadRequestException("Email already exists.", HttpStatusCode.BadRequest.ToString());
        }

        var userObject = _mapper.Map<User>(createUserDto);
        userObject.EmailConfirmed = true;
        userObject.UserName = userObject.Email;
        userObject.StartingWeight = createUserDto.Weight;
        userObject.PhoneNumberConfirmed = true;
        userObject.LockoutEnabled = false;
        userObject.IsActive = true;
        userObject.Height = createUserDto.Height;

        // Calculate BMI
        userObject.Bmi = CalculateBmi(userObject.Height, userObject.StartingWeight);

        _logger.LogInformation($"\n----------- profile doesn't exist, creating user Account | {_dateTime} ----------\n".ToUpper());

        var response = await _userManager.CreateAsync(userObject, createUserDto.Password);

        if (!response.Succeeded)
        {
            var error = response?.Errors?.First();
            throw new FitByBitBadRequestException(error!.Description, HttpStatusCode.BadRequest.ToString());
        }

        // Add the user to the "User" role
        //await _userManager.AddToRoleAsync(userObject, "User");

        _logger.LogInformation($"\n------------- Account registration completed | {_dateTime} ------------\n".ToUpper());

        // Generate JWT token
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, createUserDto.Email),
            new Claim(ClaimTypes.NameIdentifier, userObject.Id)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresIn = _configuration.GetSection("JwtSettings").GetValue<double>("Expires");

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(expiresIn),
            signingCredentials: credentials
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        var tokenData = new CreateJwtTokenDto
        {
            AccessToken = accessToken
        };

        return new GenericResponse<CreateJwtTokenDto>
        {
            Data = tokenData,
            Message = $"Account Created Successfully",
            Success = true,
            StatusCode = HttpStatusCode.Created
        };
    }

    public async Task<GenericResponse<List<UserDto>>> GetAllUsersAsync()
    {
        var userList = await _userManager.Users.ToListAsync();
        var userObjects = _mapper.Map<List<UserDto>>(userList);

        return new GenericResponse<List<UserDto>>()
        {
            Success = true,
            Message = "Success",
            Data = userObjects,
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<GenericResponse<UserDto>> GetUserByIdAsync(string id)
    {
        try
        {
            var userExist = await _userManager.FindByIdAsync(id);
            if (userExist != null)
            {
                var userObject = _mapper.Map<UserDto>(userExist);
                return new GenericResponse<UserDto>()
                {
                    Success = true,
                    Message = "Success",
                    Data = userObject,
                    StatusCode = HttpStatusCode.OK
                };
            }
            _logger.LogInformation($"\n----- User with Id {id} does not exist | {_dateTime} ------\n".ToUpper());
            throw new FitByBitNotFoundException($"User with Id {id} does not exist", HttpStatusCode.NotFound.ToString());
        }
        catch (Exception exception)
        {
            _logger.LogInformation($"\n------ {exception.Message} | {_dateTime} -------\n");
            throw new FitByBitServiceUnavailableException($"{exception.Message}: service unavailable.",
                HttpStatusCode.InternalServerError.ToString());
        }
    }

    public async Task<GenericResponse<GeneralResponse>> UpdateUserAsync(string id, UpdateUserDto updateUserDto)
    {
        try
        {
            var userObject = await _userManager.FindByIdAsync(id);
            if (userObject != null)
            {
                var userDomain = _mapper.Map(updateUserDto, userObject);
                var response = await _userManager.UpdateAsync(userDomain);
                if (response.Succeeded)
                {
                    return new GenericResponse<GeneralResponse>()
                    {
                        Success = true,
                        Message = "Data successfully updated",
                        StatusCode = HttpStatusCode.OK
                    };
                }
                _logger.LogInformation($"\n----- Could not update data, please try again.t | {_dateTime} ------\n".ToUpper());
                throw new FitByBitNotFoundException($"Could not update data, please try again.", HttpStatusCode.BadRequest.ToString());
            }
            _logger.LogInformation($"\n----- User with Id {id} does not exist | {_dateTime} ------\n".ToUpper());
            throw new FitByBitNotFoundException($"User with Id {id} does not exist", HttpStatusCode.NotFound.ToString());
        }
        catch (Exception exception)
        {
            _logger.LogInformation($"\n------ {exception.Message} | {_dateTime} -------\n");
            throw new FitByBitServiceUnavailableException($"{exception.Message}: service unavailable.",
                HttpStatusCode.InternalServerError.ToString());
        }
    }

    public async Task<GenericResponse<GeneralResponse>> DeleteUserAsync(string id)
    {
        try
        {
            var userExist = await _userManager.FindByIdAsync(id);
            if (userExist != null)
            {
                var response = await _userManager.DeleteAsync(userExist);
                if (response.Succeeded)
                {
                    return new GenericResponse<GeneralResponse>()
                    {
                        Success = true,
                        Message = "Record successfully deleted",
                        StatusCode = HttpStatusCode.OK
                    };
                }
                _logger.LogInformation($"\n----- Could not delete data, please try again. | {_dateTime} ------\n".ToUpper());
                throw new FitByBitNotFoundException($"could not delete data, please try again.", HttpStatusCode.BadRequest.ToString());
            }
            _logger.LogInformation($"\n----- User with Id {id} does not exist | {_dateTime} ------\n".ToUpper());
            throw new FitByBitNotFoundException($"User with Id {id} does not exist", HttpStatusCode.NotFound.ToString());
        }
        catch (Exception exception)
        {
            _logger.LogInformation($"\n------ {exception.Message} | {_dateTime} -------\n");
            throw new FitByBitServiceUnavailableException($"{exception.Message}: service unavailable.",
                HttpStatusCode.InternalServerError.ToString());
        }
    }

    public async Task<GenericResponse<CreateJwtTokenDto>> LoginAsync(LoginDto loginDto)
    {
        _logger.LogInformation($"\n-------------- initiate user log in process | {_dateTime} --------------\n".ToUpper());

        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
        {
            _logger.LogInformation($"\n----- User detail is invalid, please try again with correct details | {_dateTime} -----\n".ToUpper());
            throw new FitByBitNotFoundException("User detail is invalid, please try again with correct details.");
        }

        if (!user.EmailConfirmed)
        {
            _logger.LogInformation($"\n--------------- Initiate Account verification process for | {user.Email} | {_dateTime} -------------\n".ToUpper());
            var otpObject = await _generateOtpHandler.GenerateOtp(user);

            var mailContent = new SendMailNotificationEvent()
            {
                ReceiverEmail = otpObject.User.Email,
                EmailSubject = $"Account Verification Otp {otpObject.Code}",
                HtmlEmailMessage = $"Here is your account verification code: {otpObject.Code} ." + "\n" +
                          "Note: This code is only valid for 10 minutes.",
                Attachments = null,
            };
            _logger.LogInformation($"\n--------------- published mail to mediator | {_dateTime} ------------\n");

            _mediator.Publish(mailContent);

            _logger.LogInformation($"\n----- your profile is not verified, check your email {user.Email} for verification otp | {_dateTime} -----\n");
            throw new FitByBitBadRequestException($"your profile is not verified, check your email {user.Email} for verification otp", HttpStatusCode.BadRequest.ToString());
        };

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);

        if (result.IsLockedOut && user.AccessFailedCount == 2)
        {
            _logger.LogInformation($"\n---------------Account Locked after three login attempts | {_dateTime} ----------------\n".ToUpper());
            throw new FitByBitUnAuthorizedException("Your account has been locked, you exceeded the maximum failed password attempt. Kindly unlock your account by resetting your password.", HttpStatusCode.Unauthorized.ToString());
        }

        if (result.Succeeded)
        {
            _logger.LogInformation($"\n--------------- login successful {user.Email} | {_dateTime} ----------------\n".ToUpper());
            //user.LockoutEnd = null;
            //await _userManager.ResetAccessFailedCountAsync(user);
            //var roles = await _userManager.GetRolesAsync(user);
            //var claims = new List<Claim>();

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            /*            foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                            claims.Add(new Claim(ClaimTypes.Email, user.Email));
                            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        }*/

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresIn = _configuration.GetSection("JwtSettings").GetValue<double>("Expires");

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiresIn),
                signingCredentials: credentials
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var userObj = _mapper.Map<UserDto>(user);

            var data = new CreateJwtTokenDto()
            {
                //User = userObj,
                AccessToken = accessToken,
                // Roles = roles.ToArray() // Add the roles to the response
            };

            var tokenData = _mapper.Map<CreateJwtTokenDto>(data);

            return new GenericResponse<CreateJwtTokenDto>
            {
                Success = true,
                Message = "Token Fetched Successfully",
                Data = tokenData,
                StatusCode = HttpStatusCode.OK
            };
        }
        await _userManager.AccessFailedAsync(user);
        _logger.LogInformation($"\n--------------- invalid login credentials | {_dateTime} ----------------\n".ToUpper());
        throw new FitByBitBadRequestException("Invalid user and password combination, please try again.", HttpStatusCode.BadRequest.ToString());
    }

    public async Task<GenericResponse<GeneralResponse>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        _logger.LogInformation($"\n--------------- password reset request process initiated | {_dateTime} ----------------\n".ToUpper());
        var userObject = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

        if (userObject == null)
        {
            throw new FitByBitNotFoundException("User with email provided does not exist.", HttpStatusCode.NotFound.ToString());
        }
        _logger.LogInformation($"\n--------------- generating otp | {_dateTime} ----------------\n".ToUpper());

        var otpObjectDomain = await _generateOtpHandler.GenerateOtp(userObject);

        _logger.LogInformation($"\n--------------- otp generated. | {_dateTime} ----------------\n".ToUpper());

        var mailContent = new SendMailNotificationEvent()
        {
            ReceiverEmail = otpObjectDomain.User.Email,
            EmailSubject = $"Password reset Otp {otpObjectDomain.Code}",
            HtmlEmailMessage = $"Here is your password reset code: {otpObjectDomain.Code}" + "\n" +
                                    "Note: This code is only valid for 10 minutes.",
            Attachments = null,
        };
        _logger.LogInformation($"\n--------------- mail published to mediator | {_dateTime} ----------------\n".ToUpper());

        _mediator.Publish(mailContent);

        return new GenericResponse<GeneralResponse>
        {
            Message = $"Password reset otp code sent to your email {resetPasswordDto.Email}",
            Success = true,
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<GenericResponse<GeneralResponse>> SetPasswordAsync(SetPasswordDto setPasswordDto)
    {
        _logger.LogInformation($"\n-------------- password reset process initiated | {_dateTime} ---------------\n".ToUpper());

        var user = await _userManager.FindByEmailAsync(setPasswordDto.EmailAddress);

        if (user != null)
        {
            try
            {
                string encryptedOtp = CommonEncryptionHandler.Encrypt(setPasswordDto.OtpCode, _otpSecretKey);

                var otpUserObject = await _dbContext.VerificationOtps
                .Include(otp => otp.User)
                .Where(x => x.Code == encryptedOtp && x.User.Id == user.Id)
                .FirstOrDefaultAsync();

                _logger.LogInformation($"\n--------- checking for otp validity | {_dateTime} ---------\n".ToUpper());

                if (otpUserObject != null)
                {
                    if (otpUserObject.Expiry < DateTimeOffset.Now.ToLocalTime())
                    {
                        throw new FitByBitBadRequestException("Otp has expired", HttpStatusCode.BadRequest.ToString());
                    }

                    if (setPasswordDto.Password == setPasswordDto.ConfirmPassword)
                    {
                        var hashedPassword = _userManager.PasswordHasher.HashPassword(user, setPasswordDto.Password);
                        user.PasswordHash = hashedPassword;
                        user.LockoutEnd = null;

                        _logger.LogInformation($"\n--------- Setting and updating password | {_dateTime} -----------\n".ToUpper());

                        var result = await _userManager.UpdateAsync(user);
                        await _dbContext.SaveChangesAsync();

                        _logger.LogInformation($"\n--------- record updated | {_dateTime} -----------\n".ToUpper());

                        if (result.Succeeded)
                        {
                            await _userManager.ResetAccessFailedCountAsync(user);

                            _logger.LogInformation($"\n---------- password reset for {user.Email} successful | {_dateTime} ---------\n".ToUpper());

                            return new GenericResponse<GeneralResponse>()
                            {
                                Success = true,
                                Message = "Password reset successful.",
                                StatusCode = HttpStatusCode.Accepted
                            };
                        }
                        _logger.LogInformation($"\n----- Password reset failed. | {_dateTime} ------\n");
                        throw new FitByBitBadRequestException("Password reset failed.", HttpStatusCode.BadRequest.ToString());
                    }
                    _logger.LogInformation($"\n----- Passwords do not match | {_dateTime} ------\n");
                    throw new FitByBitBadRequestException("Passwords do not match", HttpStatusCode.BadRequest.ToString());
                }
                _logger.LogInformation($"\n--------------- invalid otp | {_dateTime} -------------\n".ToUpperInvariant());
                throw new FitByBitBadRequestException("Otp is not valid.", HttpStatusCode.BadRequest.ToString());
            }
            catch (Exception exception)
            {
                _logger.LogInformation($"\n---------- {exception.Message} | {_dateTime} -----------\n".ToUpper());
                throw new FitByBitSystemErrorException("An error occured while setting password, please try again.", HttpStatusCode.InternalServerError.ToString());
            }
        }
        _logger.LogInformation($"\n---------- invalid email provided ----------- | {setPasswordDto.EmailAddress} | {_dateTime}\n");
        throw new FitByBitNotFoundException($"User detail is invalid, please try again with correct details {setPasswordDto.EmailAddress}");
    }

    public async Task<GenericResponse<bool>> ValidateTokenAsync(HttpContext httpContext)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(tokenString))
            {
                // Token is missing
                return new GenericResponse<bool>
                {
                    Success = false,
                    Message = "Token is missing",
                    Data = false,
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }

            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]);
            tokenHandler.ValidateToken(tokenString, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JwtSettings:Audience"],
                ValidateLifetime = true
            }, out SecurityToken validatedToken);

            return new GenericResponse<bool>
            {
                Success = true,
                Message = "Token is valid",
                Data = true,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            // Log exception
            _logger.LogError($"Failed to validate token: {ex.Message}");
            return new GenericResponse<bool>
            {
                Success = false,
                Message = "Token is not valid",
                Data = false,
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }

    public async Task<GenericResponse<bool>> ValidateEmailAsync(string email)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return new GenericResponse<bool>
                {
                    Success = false,
                    Message = "User does not exist",
                    Data = false,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            return new GenericResponse<bool>
            {
                Success = true,
                Message = "Email is valid",
                Data = true,
                StatusCode = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            // Log exception
            _logger.LogError($"Failed to validate email address: {ex.Message}");
            return new GenericResponse<bool>
            {
                Success = false,
                Message = "Email address does not exist",
                Data = false,
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }
    

    private double CalculateBmi(double height, double weight)
    {
        // Formula for BMI: BMI = weight (kg) / (height (m) * height (m))
        return Math.Round(weight / (height * height), 2);
    }
}
