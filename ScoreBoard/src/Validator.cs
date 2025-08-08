using ScoreBoard.Exceptions;
using ScoreBoard.Interface;

namespace ScoreBoard;

public static class Validator
{
    public static void ValidateSameTeamName(string homeTeamName, string awayTeamName, Exception toThrow)
    {
        if (homeTeamName == awayTeamName)
        {
            throw toThrow;
        }
    }
    public static void ValidateTeamName(string teamName, Exception toThrow) 
    {
        if (String.IsNullOrEmpty(teamName) || teamName.GetType() != typeof(string))
        {
            throw toThrow;
        }
    }
    
    public static void ValidateMatchScore(int score, Exception toThrow)
    {
        if (score.GetType() != typeof(int) || score < 0)
        {
            throw toThrow;
            throw new MatchException("Team score not in correct format.");
        }
    }
    
    public static void ValidateMatchUpdate((int home, int away) currentScore, (int home, int away) toUpdateScore, Exception toThrow)
    {
        if (toUpdateScore.home < currentScore.home|| toUpdateScore.away < currentScore.away)
        {
            throw toThrow;
            throw new MatchException("Score can not reduce.");
        }
    }
    
    public static void ValidateMatchCompare(IMatch match , Exception toThrow)
    {
        if (!(match is Match))
        {
            throw toThrow;
            throw new MatchException("Match type not in correct format.");
        }
    }
    
    public static void ValidateMatchId(Guid matchId, Exception toThrow)
    {
        if (matchId.GetType() != typeof(Guid))
        {
            throw toThrow;
        }
    }
}