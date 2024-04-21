namespace FitByBitApiService.Exceptions;

public class FitByBitNotFoundException : Exception
{
    public string Code { get; set; } = null!;

    public FitByBitNotFoundException()
    {

    }


    public FitByBitNotFoundException(string message) : base(message)
    {

    }

    public FitByBitNotFoundException(string message, string code) : base(message)
    {
        Code = code;
    }

    public FitByBitNotFoundException(string message, Exception inner) : base(message, inner)
    {

    }
}
