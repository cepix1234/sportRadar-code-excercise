using ScoreBoard.Interface;

namespace ScoreBoard.test.UnitTest_ScoreBoard;

[TestFixture]
public class UnitTestScoreBoardFinishMatch
{
    private ScoreBoardTest _scoreBoard;
    static string _home = "Spain";
    static string _away = "Brazil";
    private Guid matchGuid;

    [SetUp]
    public void SetUp()
    {
        _scoreBoard = new ScoreBoardTest();
        matchGuid = _scoreBoard.StartMatch(_home, _away);
    }

    [Test]
    public void FishMatch_CorrectlyRemoveItByProvidedMatchGuid()
    {
        _scoreBoard.FinishMatch(matchGuid);
        _scoreBoard._matches.Keys.Contains(matchGuid);
        Assert.Equals(_scoreBoard._matches.Keys.Contains(matchGuid), false);
    }
    
    [TestCase("Spain")]
    [TestCase("Brazil")]
    public void FishMatch_CorrectlyRemoveItByAnyProvidedTeamName(string teamName)
    {
        _scoreBoard.FinishMatch(teamName);
        _scoreBoard._matches.Keys.Contains(matchGuid);
        Assert.Equals(_scoreBoard._matches.Keys.Contains(matchGuid), false);
    }
    
    [Test]
    public void FishMatch_ThrowsException_ProvidedMatchGuidDesNotExist()
    {
        Assert.Throws<Exception>(() => _scoreBoard.FinishMatch(Guid.NewGuid())); 
    }
    
    [TestCase("SpainA")]
    [TestCase("BrazilA")]
    public void FishMatch_ThrowsException_ProvidedTeamNameDoesNotExist(string teamName)
    {
        Assert.Throws<Exception>(() => _scoreBoard.FinishMatch(teamName)); 
    }
    
#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase("SpainB")]
    [TestCase(null)]
    [TestCase(1)]
#pragma warning restore NUnit1001
    public void FishMatch_ThrowsException_ProvidedMatchGuidIsNotInCorrectFormat(Guid matchGuid)
    {
        Assert.Throws<Exception>(() => _scoreBoard.FinishMatch(matchGuid));
    }

#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase(1)]
    [TestCase(null)]
#pragma warning restore NUnit1001
    public void FishMatch_ThrowsException_ProvidedTeamNameIsNotInCorrectFormat(string teamName)
    {
        Assert.Throws<Exception>(() => _scoreBoard.FinishMatch(teamName)); 
    }
}