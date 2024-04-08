using AutoMapper;
using FitByBitService.Entities.Models;
using FitByBitService.Entities.Responses;
//using FitByBitService.Entities.Responses.DriverResponse;
using FitByBitService.Entities.Responses.RoleResponse;
using FitByBitService.Entities.Responses.UserResponse;
//using FitByBitService.Entities.Responses.VendorResponse;
using Microsoft.AspNetCore.Identity;

namespace FitByBitService.Mappings;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        // Mappings for Users.
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<UpdateUserDto, User>();
        CreateMap<IdentityResult, UserDto>().ReverseMap();
        CreateMap<IdentityUserRole<string>, AddUserToRoleDto>().ReverseMap();
        
        // Map otp
        CreateMap<VerificationOtp, VerificationOtpDto>().ReverseMap();
        
        // Mappings for roles.
        CreateMap<RolesDto, IdentityRole>().ReverseMap();
        CreateMap<CreateRoleDto, IdentityRole>().ReverseMap();
        CreateMap<IdentityResult, RolesDto>().ReverseMap();
        CreateMap<UpdateRoleDto, IdentityRole>();
        
    }
}
