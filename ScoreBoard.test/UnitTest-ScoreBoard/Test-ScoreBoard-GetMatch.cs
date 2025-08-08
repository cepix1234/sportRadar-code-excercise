using ScoreBoard.Exceptions;

namespace ScoreBoard.test.UnitTest_ScoreBoard;


[TestFixture]
public class UnitTestScoreBoardGetMatch
{
    private ScoreBoard _scoreBoard;
    private string _home = "Spain";
    private string _away = "Brazil";
    private Guid _matchGuid;

    [SetUp]
    public void SetUp()
    {
        _scoreBoard = new ScoreBoard();
        _matchGuid = _scoreBoard.StartMatch(_home, _away);
        _scoreBoard.StartMatch("A", "B");
    }

    [TestCase("Spain")]
    [TestCase("Brazil")]
    public void GetMatch_GetsCorrectMatchByAnyTeamName(string teamName)
    {
        Guid gottenGuid = _scoreBoard.GetMatch(teamName);
        Assert.That(gottenGuid, Is.EqualTo(_matchGuid));
    }
    
    [TestCase("SpainA")]
    [TestCase("BrazilA")]
    public void GetMatch_ThrowsException_ProvidedTeamNameDoesNotExist(string teamName)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.GetMatch(teamName)); 
    }

#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase(null)]
#pragma warning restore NUnit1001
    [TestCase("")]
    public void GetMatch_ThrowsException_ProvidedTeamNameIncorrectFormat(string teamName)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.GetMatch(teamName));
    }
}