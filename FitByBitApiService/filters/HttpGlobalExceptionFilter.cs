using System.Net;
using FitByBitService.Exceptions;
using FitByBitService.Extensions;
using FitByBitService.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FitByBitService.filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);
            _logger.LogInformation($"Environment: {_env.EnvironmentName}");
            HttpStatusCode code;
            ExceptionGenericResponse response;

            switch (context.Exception)
            {
                case FitByBitNotFoundException e:
                    code = HttpStatusCode.NotFound;
                    response = e.ToErrorResponse();
                    break;
                case FitByBitServiceUnavailableException e:
                    code = HttpStatusCode.BadRequest;
                    response = e.ToErrorResponse();
                    break;
                case FitByBitUnAuthorizedException e:
                    code = HttpStatusCode.Unauthorized;
                    response = e.ToErrorResponse();
                    break;
                case FitByBitForbiddenException e:
                    code = HttpStatusCode.Forbidden;
                    response = e.ToErrorResponse();
                    break;
                case FitByBitBadRequestException e:
                    code = HttpStatusCode.BadRequest;
                    response = e.ToErrorResponse();
                    break;
                case FitByBitObjectExistException e:
                    code = HttpStatusCode.BadRequest;
                    response = e.ToErrorResponse();
                    break;
                case FitByBitSystemErrorException e:
                    code = HttpStatusCode.InternalServerError;
                    response = e.ToErrorResponse();
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    response = context.Exception.ToErrorResponse();
                    break;
            }

            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            var serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
            var result = JsonConvert.SerializeObject(response, serializerSettings);
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.HttpContext.Response.WriteAsync(result);
            context.ExceptionHandled = true;
        }

    }
}
