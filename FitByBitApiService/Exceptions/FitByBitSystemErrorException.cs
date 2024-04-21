namespace FitByBitApiService.Exceptions;

public class FitByBitSystemErrorException : Exception
{
    public string Code { get; set; } = null!;

    public FitByBitSystemErrorException()
    {

    }

    public FitByBitSystemErrorException(string message) : base(message)
    {

    }

    public FitByBitSystemErrorException(string message, string code) : base(message)
    {
        Code = code;
    }

    public FitByBitSystemErrorException(string message, Exception inner) : base(message, inner)
    {

    }
}
