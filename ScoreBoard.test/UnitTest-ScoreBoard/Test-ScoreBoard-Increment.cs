using ScoreBoard.Exceptions;
using ScoreBoard.Interface;
using ScoreBoard.test.Test_Utils;

namespace ScoreBoard.test.UnitTest_ScoreBoard;

[TestFixture]
public class UnitTestScoreBoardIncrement
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
    public void IncrementHome_CorrectlyIncrementHomeScoreWithProvidedMatchGuid()
    {
        _scoreBoard.IncrementHome(matchGuid);
        Dictionary<Guid, IMatch> result = new Dictionary<Guid, IMatch>
        {
            { matchGuid, new Match(_home, _away, (1, 0))}
        };
        Dictionary<Guid, IMatch> matches = (Dictionary<Guid,IMatch>)PrivateValueAccessor.GetPrivateFieldValue(typeof(ScoreBoard), "_matches", _scoreBoard);
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", result[matchGuid], matches[matchGuid].MatchStart);
        bool areEqual = result.Count == matches.Count && matches[matchGuid].Compare(result[matchGuid]) == 0;
        Assert.That(true, Is.EqualTo(areEqual));
    }

    [TestCase("Spain")]
    [TestCase("Brazil")]
    public void IncrementHome_CorrectlyIncrementHomeScoreWithAntProvidedTeamName(string teamName)
    {
        _scoreBoard.IncrementHome(teamName);
        Dictionary<Guid, IMatch> result = new Dictionary<Guid, IMatch>
        {
            { matchGuid, new Match(_home, _away, (1, 0))}
        };
        Dictionary<Guid, IMatch> matches = (Dictionary<Guid,IMatch>)PrivateValueAccessor.GetPrivateFieldValue(typeof(ScoreBoard), "_matches", _scoreBoard);
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", result[matchGuid], matches[matchGuid].MatchStart);
        bool areEqual = result.Count == matches.Count && matches[matchGuid].Compare(result[matchGuid]) == 0;
        Assert.That(true, Is.EqualTo(areEqual));
    }
    
    [Test]
    public void IncrementAway_CorrectlyIncrementHomeScoreWithProvidedMatchGuid()
    {
        _scoreBoard.IncrementAway(matchGuid);
        Dictionary<Guid, IMatch> result = new Dictionary<Guid, IMatch>
        {
            { matchGuid, new Match(_home, _away, (0, 1))}
        };
        Dictionary<Guid, IMatch> matches = (Dictionary<Guid,IMatch>)PrivateValueAccessor.GetPrivateFieldValue(typeof(ScoreBoard), "_matches", _scoreBoard);
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", result[matchGuid], matches[matchGuid].MatchStart);
        bool areEqual = result.Count == matches.Count && matches[matchGuid].Compare(result[matchGuid]) == 0;
        Assert.That(true, Is.EqualTo(areEqual));
    }

    [TestCase("Spain")]
    [TestCase("Brazil")]
    public void IncrementAway_CorrectlyIncrementHomeScoreWithAntProvidedTeamName(string teamName)
    {
        _scoreBoard.IncrementHome(teamName);
        Dictionary<Guid, IMatch> result = new Dictionary<Guid, IMatch>
        {
            { matchGuid, new Match(_home, _away, (0, 1))}
        };
        Dictionary<Guid, IMatch> matches = (Dictionary<Guid,IMatch>)PrivateValueAccessor.GetPrivateFieldValue(typeof(ScoreBoard), "_matches", _scoreBoard);
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", result[matchGuid], matches[matchGuid].MatchStart);
        bool areEqual = result.Count == matches.Count && matches[matchGuid].Compare(result[matchGuid]) == 0;
        Assert.That(true, Is.EqualTo(areEqual));
    }
    
    [Test]
    public void IncrementHome_ThrowsException_ProvidedMatchGuidDesNotExist()
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.IncrementHome(Guid.NewGuid()));
    }

    [TestCase("SpainA")]
    [TestCase("BrazilA")]
    public void IncrementHome_ThrowsException_ProvidedTeamNameDoesNotExist(string teamName)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.IncrementHome(teamName)); 
    }
    
    [Test]
    public void IncrementAway_ThrowsException_ProvidedMatchGuidDesNotExist()
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.IncrementAway(Guid.NewGuid()));
    }

    [TestCase("SpainA")]
    [TestCase("BrazilA")]
    public void IncrementAway_ThrowsException_ProvidedTeamNameDoesNotExist(string teamName)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.IncrementAway(teamName)); 
    }
    
#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase(null)]
#pragma warning restore NUnit1001
    public void IncrementHome_ThrowsException_ProvidedMatchGuidIsNotInCorrectFormat(Guid matchGuid)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.IncrementHome(matchGuid));
    }

#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase(null)]
#pragma warning restore NUnit1001
    public void IncrementHome_ThrowsException_ProvidedTeamNameIsNotInCorrectFormat(string teamName)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.IncrementHome(teamName)); 
    }
    
#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase(null)]
#pragma warning restore NUnit1001
    public void IncrementAway_ThrowsException_ProvidedMatchGuidIsNotInCorrectFormat(Guid matchGuid)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.IncrementAway(matchGuid));
    }

#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase(null)]
#pragma warning restore NUnit1001
    public void IncrementAway_ThrowsException_ProvidedTeamNameIsNotInCorrectFormat(string teamName)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.IncrementAway(teamName)); 
    }
}