namespace ScoreBoard.test.UnitTest_Match;

public class MatchTest : Match
{
    public MatchTest(string homeTeamName, string awayTeamName) : base(homeTeamName, awayTeamName)
     {}
     
     public MatchTest(string homeTeamName, string awayTeamName, (int home, int away) score): base(homeTeamName, awayTeamName, score)
     {}
    public int _homeScore;
    public int _awayScore;
    public string _homeTeamName;
    public string _awayTeamName;
    public long _matchStart;
}