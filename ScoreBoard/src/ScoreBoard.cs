using ScoreBoard.Interface;

namespace ScoreBoard;

public class ScoreBoard : IScoreBoard
{
    public Guid StartMatch(string homeTeam, string awayTeam)
    {
        throw new NotImplementedException();
    }

    public Guid StartMatch(string homeTeam, string awayTeam, (int home, int away) score)
    {
        throw new NotImplementedException();
    }

    public Guid GetMatch(string teamName)
    {
        throw new NotImplementedException();
    }

    public void UpdateMatch(Guid matchId, (int home, int away) score)
    {
        throw new NotImplementedException();
    }

    public void UpdateMatch(string teamName, (int home, int away) score)
    {
        throw new NotImplementedException();
    }

    public void AddScore(Guid matchId, (int home, int away) score)
    {
        throw new NotImplementedException();
    }

    public void AddScore(string teamName, (int home, int away) score)
    {
        throw new NotImplementedException();
    }

    public void IncrementHome(Guid matchId)
    {
        throw new NotImplementedException();
    }

    public void IncrementHome(string teamName)
    {
        throw new NotImplementedException();
    }

    public void IncrementAway(Guid matchId)
    {
        throw new NotImplementedException();
    }

    public void IncrementAway(string teamName)
    {
        throw new NotImplementedException();
    }

    public void FinishMatch(Guid matchId)
    {
        throw new NotImplementedException();
    }

    public void FinishMatch(string teamName)
    {
        throw new NotImplementedException();
    }

    public string Summary()
    {
        throw new NotImplementedException();
    }
}
