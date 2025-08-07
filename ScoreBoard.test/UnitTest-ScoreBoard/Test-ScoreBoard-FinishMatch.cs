using ScoreBoard.Exceptions;
using ScoreBoard.Interface;
using ScoreBoard.test.Test_Utils;

namespace ScoreBoard.test.UnitTest_ScoreBoard;

[TestFixture]
public class UnitTestScoreBoardFinishMatch
{
    private ScoreBoard _scoreBoard;
    static string _home = "Spain";
    static string _away = "Brazil";
    private Guid matchGuid;

    [SetUp]
    public void SetUp()
    {
        _scoreBoard = new ScoreBoard();
        matchGuid = _scoreBoard.StartMatch(_home, _away);
    }

    [Test]
    public void FishMatch_CorrectlyRemoveItByProvidedMatchGuid()
    {
        _scoreBoard.FinishMatch(matchGuid);
        Dictionary<Guid, IMatch> matches = (Dictionary<Guid,IMatch>)PrivateValueAccessor.GetPrivateFieldValue(typeof(ScoreBoard), "_matches", _scoreBoard);
        Assert.Equals(matches.Keys.Contains(matchGuid), false);
    }
    
    [TestCase("Spain")]
    [TestCase("Brazil")]
    public void FishMatch_CorrectlyRemoveItByAnyProvidedTeamName(string teamName)
    {
        _scoreBoard.FinishMatch(teamName);
        Dictionary<Guid, IMatch> matches = (Dictionary<Guid,IMatch>)PrivateValueAccessor.GetPrivateFieldValue(typeof(ScoreBoard), "_matches", _scoreBoard);
        Assert.Equals(matches.Keys.Contains(matchGuid), false);
    }
    
    [Test]
    public void FishMatch_ThrowsException_ProvidedMatchGuidDesNotExist()
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.FinishMatch(Guid.NewGuid())); 
    }
    
    [TestCase("SpainA")]
    [TestCase("BrazilA")]
    public void FishMatch_ThrowsException_ProvidedTeamNameDoesNotExist(string teamName)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.FinishMatch(teamName)); 
    }
    
#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase("SpainB")]
    [TestCase(null)]
    [TestCase(1)]
#pragma warning restore NUnit1001
    public void FishMatch_ThrowsException_ProvidedMatchGuidIsNotInCorrectFormat(Guid matchGuid)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.FinishMatch(matchGuid));
    }

#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase(1)]
    [TestCase(null)]
#pragma warning restore NUnit1001
    public void FishMatch_ThrowsException_ProvidedTeamNameIsNotInCorrectFormat(string teamName)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.FinishMatch(teamName)); 
    }
}