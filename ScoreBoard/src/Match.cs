using ScoreBoard.Interface;

namespace ScoreBoard;

public class Match : IMatch
{
    public Match(string homeTeamName, string awayTeamName)
    {
        throw new NotImplementedException();
    }

    public Match(string homeTeamName, string awayTeamName, (int home, int away) score)
    {
        throw new NotImplementedException();
    }

    public void Update(int home, int away)
    {
        throw new NotImplementedException();
    }

    public void Add(int home, int away)
    {
        throw new NotImplementedException();
    }

    public void IncrementHome()
    {
        throw new NotImplementedException();
    }

    public void IncrementAway()
    {
        throw new NotImplementedException();
    }

    public int Compare(IMatch match)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        throw new NotImplementedException();
    }
}
