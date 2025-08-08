using ScoreBoard.Exceptions;
using ScoreBoard.test.Test_Utils;

namespace ScoreBoard.test.UnitTest_Match;

[TestFixture]
public class UnitTestMatchScoreUpdate
{
    private Match _match;

    [SetUp]
    public void SetUp()
    {
        _match = new Match("A", "B");
    }

    [TestCase(1, 0)]
    [TestCase(2, 0)]
    [TestCase(0, 1)]
    [TestCase(1, 1)]
    [TestCase(0, 2)]
    public void MatchUpdate_UpdateScore_IsCorrectlyUpdated(int home, int away)
    {
        _match.Update(home, away);
        int currentHome = (int)PrivateValueAccessor.GetPrivateFieldValue(typeof(Match), "_homeScore", _match);
        int currentAway = (int)PrivateValueAccessor.GetPrivateFieldValue(typeof(Match), "_awayScore", _match);
        Assert.That(home, Is.EqualTo(currentHome));
        Assert.That(away, Is.EqualTo(currentAway));
    }

    [TestCase(-1, 0)]
    [TestCase(0, -1)]
    [TestCase(-1, -1)]
    public void MatchUpdate_UpdateScore_ThrowsException_ProvidedScoreMustBeAnAbsoluteNumber(int home, int away)
    {
        Assert.Throws<MatchException>(() => _match.Update(home, away));
    }
}
