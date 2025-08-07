using ScoreBoard.Exceptions;
using ScoreBoard.Interface;
using ScoreBoard.test.Test_Utils;

namespace ScoreBoard.test.UnitTest_ScoreBoard;


[TestFixture]
public class UnitTestScoreBoardStartMatch
{
    private ScoreBoard _scoreBoard;
    private string _home = "Spain";
    private string _away = "Brazil";

    [SetUp]
    public void SetUp()
    {
        _scoreBoard = new ScoreBoard();
    }

    [Test]
    public void NewMatchStart_WithoutIssue()
    {
        Assert.DoesNotThrow(() => _scoreBoard.StartMatch(_home, _away));
    }

    [Test]
    public void NewMatchStart_ThrowsException_MatchWithProvidedTeamsExists()
    {
        _scoreBoard.StartMatch(_home, _away);
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.StartMatch(_home, _away));
    }

    [Test]
    public void NewMatchStart_WithCustomInitialScore()
    {
        Guid matchId = _scoreBoard.StartMatch(_home, _away, (home: 1, away: 2));
        Dictionary<Guid, IMatch> result = new Dictionary<Guid, IMatch>
        {
            { matchId, new Match(_home, _away, (home:1, away:2))}
        };
        Dictionary<Guid, IMatch> matches = (Dictionary<Guid,IMatch>)PrivateValueAccessor.GetPrivateFieldValue(typeof(ScoreBoard), "_matches", _scoreBoard);
        bool areEqual = result.Count == matches.Count && !matches.Except(result).Any();
        Assert.Equals(areEqual, true);
    }
    
    [Test]
    public void NewMatchStart_ThrowsException_HomeTeamAndAwayTeamShouldBeDifferent()
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.StartMatch("A", "A"));
    }
    
#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase("A", null)]
    [TestCase(null, "B")]
    [TestCase(null,null)]
    [TestCase(1,"A")]
    [TestCase("A",1)]
    [TestCase(1,1)]
#pragma warning restore NUnit1001
    [TestCase("","B")]
    [TestCase("A","")]
    [TestCase("","")]
    public void NewMatchStart_ThrowsException_ProvidedTeamNameIncorrectFormat(string home, string away)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.StartMatch(home, away));
    }
    
#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase(1, null)]
    [TestCase(null, 1)]
    [TestCase(null, null)]
    [TestCase("A", 1)]
    [TestCase(1,"A")]
    [TestCase("A", "A")]
#pragma warning restore NUnit1001
    public void NewMatchStart_WithCustomInitialScore_ThrowsException_ProvidedScoreMustBeAnAbsoluteNumber(int homeScore, int awayScore)
    {
        Assert.Throws<ScoreBoardException>(() => _scoreBoard.StartMatch(_home, _away, (homeScore, awayScore)));
    }
}