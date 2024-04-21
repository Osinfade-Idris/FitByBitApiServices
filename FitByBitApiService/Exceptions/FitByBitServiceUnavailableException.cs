namespace FitByBitApiService.Exceptions;

public class FitByBitServiceUnavailableException : Exception
{
    public string Code { get; set; } = null!;

    public FitByBitServiceUnavailableException()
    {

    }

    public FitByBitServiceUnavailableException(string message) : base(message)
    {

    }

    public FitByBitServiceUnavailableException(string message, string code) : base(message)
    {
        Code = code;
    }

    public FitByBitServiceUnavailableException(string message, Exception inner) : base(message, inner)
    {

    }
}
