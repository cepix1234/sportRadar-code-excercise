using ScoreBoard.Interface;

namespace ScoreBoard.test.UnitTest_ScoreBoard;

[TestFixture]
public class UnitTestScoreBoardAddScore
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
    public void AddScore_CorrectlyAddToMatchScoreWithProvidedGuid()
    {
        _scoreBoard.UpdateMatch(matchGuid, (1,0));
        _scoreBoard.AddScore(matchGuid, (1,1));
        Dictionary<Guid, IMatch> result = new Dictionary<Guid, IMatch>
        {
            { matchGuid, new Match(_home, _away, (2, 1))}
        };
        bool areEqual = result.Count == _scoreBoard._matches.Count && !_scoreBoard._matches.Except(result).Any();
        Assert.Equals(areEqual, true);
    }

    [TestCase("Spain")]
    [TestCase("Brazil")]
    public void AddScore_CorrectlyAddToMatchScoreWithAnyProvidedTeamName(string teamName)
    {
        _scoreBoard.UpdateMatch(teamName, (1,0));
        _scoreBoard.AddScore(teamName, (1,1));
        Dictionary<Guid, IMatch> result = new Dictionary<Guid, IMatch>
        {
            { matchGuid, new Match(_home, _away, (2, 1))}
        };
        bool areEqual = result.Count == _scoreBoard._matches.Count && !_scoreBoard._matches.Except(result).Any();
        Assert.Equals(areEqual, true);
    }
    
    [Test]
    public void AddScore_ThrowsException_ProvidedMatchGuidDesNotExist()
    {
        Assert.Throws<Exception>(() => _scoreBoard.AddScore(Guid.NewGuid(), (1, 1)));
    }

    [TestCase("SpainA")]
    [TestCase("BrazilA")]
    public void AddScore_ThrowsException_ProvidedTeamNameDoesNotExist(string teamName)
    {
        Assert.Throws<Exception>(() => _scoreBoard.AddScore(teamName, (1, 1))); 
    }
    
#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase("SpainB")]
    [TestCase(null)]
    [TestCase(1)]
#pragma warning restore NUnit1001
    public void AddScore_ThrowsException_ProvidedMatchGuidIsNotInCorrectFormat(Guid matchGuid)
    {
        Assert.Throws<Exception>(() => _scoreBoard.AddScore(matchGuid, (1, 1)));
    }

#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase(1)]
    [TestCase(null)]
#pragma warning restore NUnit1001
    public void AddScore_ThrowsException_ProvidedTeamNameIsNotInCorrectFormat(string teamName)
    {
        Assert.Throws<Exception>(() => _scoreBoard.AddScore(teamName, (1, 1))); 
    }
}