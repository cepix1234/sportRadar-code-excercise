using ScoreBoard.Exceptions;
using ScoreBoard.Interface;
using ScoreBoard.test.Test_Utils;

namespace ScoreBoard.test.UnitTest_ScoreBoard;

[TestFixture]
public class UnitTestScoreBoardUpdateMatch
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
    public void UpdateMatch_CorrectlyUpdatesMatchWithProvidedGuid()
    {
        _scoreBoard.UpdateMatch(matchGuid, (1,1));
        Dictionary<Guid, IMatch> result = new Dictionary<Guid, IMatch>
        {
            { matchGuid, new Match(_home, _away, (1, 1))}
        };
        Dictionary<Guid, IMatch> matches = (Dictionary<Guid,IMatch>)PrivateValueAccessor.GetPrivateFieldValue(typeof(ScoreBoard), "_matches", _scoreBoard);
        bool areEqual = result.Count == matches.Count && !matches.Except(result).Any();
        Assert.Equals(areEqual, true);
    }

    [TestCase("Spain")]
    [TestCase("Brazil")]
    public void UpdateMatch_CorrectlyUpdatesMatchWithAnyProvidedTeamName(string teamName)
    {
        _scoreBoard.UpdateMatch(teamName, (1,1));
        Dictionary<Guid, IMatch> result = new Dictionary<Guid, IMatch>
        {
            { matchGuid, new Match(_home, _away, (1, 1))}
        };
        Dictionary<Guid, IMatch> matches = (Dictionary<Guid,IMatch>)PrivateValueAccessor.GetPrivateFieldValue(typeof(ScoreBoard), "_matches", _scoreBoard);
        bool areEqual = result.Count == matches.Count && !matches.Except(result).Any();
        Assert.Equals(areEqual, true);
    }
    
    [Test]
    public void UpdateMatch_ThrowsException_ProvidedMatchGuidDesNotExist()
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.UpdateMatch(Guid.NewGuid(), (1, 1)));
    }

    [TestCase("SpainA")]
    [TestCase("BrazilA")]
    public void UpdateMatch_ThrowsException_ProvidedTeamNameDoesNotExist(string teamName)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.UpdateMatch(teamName, (1, 1))); 
    }
    
#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase("SpainB")]
    [TestCase(null)]
    [TestCase(1)]
#pragma warning restore NUnit1001
    public void UpdateMatch_ThrowsException_ProvidedMatchGuidIsNotInCorrectFormat(Guid matchGuid)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.UpdateMatch(matchGuid, (1, 1)));
    }

#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase(1)]
    [TestCase(null)]
#pragma warning restore NUnit1001
    public void UpdateMatch_ThrowsException_ProvidedTeamNameIsNotInCorrectFormat(string teamName)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.UpdateMatch(teamName, (1, 1))); 
    }
}