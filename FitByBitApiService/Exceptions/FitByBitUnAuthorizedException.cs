namespace FitByBitApiService.Exceptions;

public class FitByBitUnAuthorizedException : Exception
{
    public string Code { get; set; } = null!;

    public FitByBitUnAuthorizedException()
    {

    }

    public FitByBitUnAuthorizedException(string message) : base(message)
    {

    }

    public FitByBitUnAuthorizedException(string message, string code) : base(message)
    {
        Code = code;
    }

    public FitByBitUnAuthorizedException(string message, Exception code) : base(message, code)
    {

    }
}
