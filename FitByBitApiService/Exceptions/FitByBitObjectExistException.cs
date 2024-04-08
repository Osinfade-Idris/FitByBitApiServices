namespace FitByBitService.Exceptions;

public class FitByBitObjectExistException : Exception
{
    public string Code { get; set; } = null!;

    public FitByBitObjectExistException()
    {
        
    }

    public FitByBitObjectExistException(string message) : base(message)
    {
        
    }

    public FitByBitObjectExistException(string message, string code) : base(message)
    {
        Code = code;
    }

    public FitByBitObjectExistException(string message, Exception inner) : base(message, inner)
    {
        
    }
    
}
