namespace ScoreBoard.Exceptions;

public class MatchExceptions: Exception 
{
    public MatchExceptions()
    {
    }

    public MatchExceptions(string message)
        : base(message)
    {
    }

    public MatchExceptions(string message, Exception inner)
        : base(message, inner)
    {
    } 
}