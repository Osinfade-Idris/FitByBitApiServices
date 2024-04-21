namespace FitByBitApiService.Exceptions;

public class FitByBitForbiddenException : Exception
{
    public string Code { get; set; } = null!;

    public FitByBitForbiddenException()
    {

    }

    public FitByBitForbiddenException(string message) : base(message)
    {

    }

    public FitByBitForbiddenException(string message, string code) : base(message)
    {
        Code = code;
    }

    public FitByBitForbiddenException(string message, Exception inner) : base(message, inner)
    {

    }
}
