namespace FitByBitApiService.Exceptions;

public class FitByBitBadRequestException : Exception
{
    public string Code { get; set; } = null!;

    public FitByBitBadRequestException() : base()
    {

    }

    public FitByBitBadRequestException(string message) : base(message)
    {

    }

    public FitByBitBadRequestException(string message, string code) : base(message)
    {
        Code = code;
    }

    public FitByBitBadRequestException(string message, Exception exception) : base(message, exception)
    {

    }
}
