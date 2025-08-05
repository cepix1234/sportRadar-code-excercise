using ScoreBoard.Interface;

namespace ScoreBoard.test.UnitTest_ScoreBoard;

public class ScoreBoardTest : ScoreBoard
{
    public Dictionary<Guid, IMatch> _matches = new Dictionary<Guid, IMatch>();
    public Dictionary<string, Guid> _matchTeams = new Dictionary<string, Guid>();
}