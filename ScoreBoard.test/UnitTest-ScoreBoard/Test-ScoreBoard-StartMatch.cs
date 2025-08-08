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
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", result[matchId], matches[matchId].MatchStart);
        bool areEqual = result.Count == matches.Count && matches[matchId].Compare(result[matchId]) == 0;
        Assert.That(true, Is.EqualTo(areEqual));
    }
    
    [Test]
    public void NewMatchStart_ThrowsException_HomeTeamAndAwayTeamShouldBeDifferent()
    {
        Assert.Throws<MatchException>(() => _scoreBoard.StartMatch("A", "A"));
    }
    
#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase("A", null, typeof(MatchException))]
    [TestCase(null, "B", typeof(MatchException))]
    [TestCase(null,null, typeof(MatchException))]
#pragma warning restore NUnit1001
    [TestCase("","B", typeof(MatchException))]
    [TestCase("A","", typeof(MatchException))]
    [TestCase("","", typeof(MatchException))]
    public void NewMatchStart_ThrowsException_ProvidedTeamNameIncorrectFormat(string home, string away, Type e)
    {
        Assert.Throws(Is.TypeOf(e),() => _scoreBoard.StartMatch(home, away));
    }
    
    [TestCase(-1, 1)]
    [TestCase(1, -1)]
    [TestCase(-1, -1)]
    public void NewMatchStart_WithCustomInitialScore_ThrowsException_ProvidedScoreMustBeAnAbsoluteNumber(int homeScore, int awayScore)
    {
        Assert.Throws<MatchException>(() => _scoreBoard.StartMatch(_home, _away, (homeScore, awayScore)));
    }
}