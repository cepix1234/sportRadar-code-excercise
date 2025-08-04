using ScoreBoard.Interface;

namespace ScoreBoard.test;

public class ScoreBoardTest : ScoreBoard
{
    public Dictionary<Guid, IMatch> _matches = new Dictionary<Guid, IMatch>();
}

[TestFixture]
public class UnitTestScoreBoard_StartMatch
{
    private ScoreBoardTest _scoreBoard;
    private string _home = "Spain";
    private string _away = "Brazil";

    [SetUp]
    public void SetUp()
    {
        _scoreBoard = new ScoreBoardTest();
    }

    [Test]
    public void NewMatchStart_WithoutIssue()
    {
        Assert.DoesNotThrow(() => _scoreBoard.StartMatch(_home, _away));
    }

    [Test]
    public void NewMatchStar_ExceptionThrownMatchExists()
    {
        _scoreBoard.StartMatch(_home, _away);
        Assert.Throws<Exception>(() => _scoreBoard.StartMatch(_home, _away));
    }

    [Test]
    public void NewMatchStar_WithCustomInitialScore()
    {
        Guid matchId = _scoreBoard.StartMatch(_home, _away, (home: 1, away: 2));
        Assert.Equals(_scoreBoard._matches[matchId].ToString()!, $"{_home}: 1 - {_away}: 2");
    }
}
