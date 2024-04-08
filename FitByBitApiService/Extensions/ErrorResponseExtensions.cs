using FitByBitService.Entities.Responses;
using FitByBitService.Exceptions;
using FitByBitService.Helpers;

namespace FitByBitService.Extensions
{
    public static class ErrorResponseExtensions
    {
        public static ExceptionGenericResponse ToErrorResponse(this FitByBitBadRequestException e)
        {
            return new ExceptionGenericResponse()
            {
                Success = false,
                StatusCode = e.Code,
                Message = e.Message
            };
        }

        public static ExceptionGenericResponse ToErrorResponse(this FitByBitForbiddenException e)
        {
            return new ExceptionGenericResponse
            {
                Success = false,
                StatusCode = e.Code,
                Message = e.Message
            };
        }

        public static ExceptionGenericResponse ToErrorResponse(this FitByBitNotFoundException e)
        {
            return new ExceptionGenericResponse
            {
                Success = false,
                StatusCode = e.Code,
                Message = e.Message
            };
        }

        public static ExceptionGenericResponse ToErrorResponse(this FitByBitObjectExistException e)
        {
            return new ExceptionGenericResponse
            {
                Success = false,
                StatusCode = e.Code,
                Message = e.Message

            };
        }

        public static ExceptionGenericResponse ToErrorResponse(this FitByBitServiceUnavailableException e)
        {
            return new ExceptionGenericResponse
            {
                Success = false,
                StatusCode = e.Code,
                Message = e.Message
            };
        }

        public static ExceptionGenericResponse ToErrorResponse(this FitByBitSystemErrorException e)
        {
            return new ExceptionGenericResponse
            {
                Success = false,
                StatusCode = e.Code,
                Message = e.Message

            };
        }

        public static ExceptionGenericResponse ToErrorResponse(this FitByBitUnAuthorizedException e)
        {
            return new ExceptionGenericResponse
            {
                Success = false,
                StatusCode = e.Code,
                Message = e.Message

            };
        }

        public static ExceptionGenericResponse ToErrorResponse(this Exception e)
        {
            return new ExceptionGenericResponse
            {
                Success = false,
                StatusCode = "SYSTEM_ERROR",
                Message = $"Unexpected error occured please try again or confirm current operation status"
            };
        }

    }
}
