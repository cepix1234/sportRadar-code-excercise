using ScoreBoard.test.Test_Utils;

namespace ScoreBoard.test.UnitTest_Match;

[TestFixture]
public class UnitTestMatchScoreIncrement
{
    private Match _match;

    [SetUp]
    public void SetUp()
    {
        _match = new Match("A", "B");
    }

    [Test]
    public void MatchIncrement_HomeScoreIsIncremented()
    {
        int originalHome = (int)PrivateValueAccessor.GetPrivateFieldValue(typeof(Match), "_homeScore", _match);
        _match.IncrementHome();
        int currentHome = (int)PrivateValueAccessor.GetPrivateFieldValue(typeof(Match), "_homeScore", _match);
        Assert.That(originalHome + 1, Is.EqualTo(currentHome));
    }

    [Test]
    public void MatchIncrement_AwayScoreIsIncremented()
    {
        int originalAway = (int)PrivateValueAccessor.GetPrivateFieldValue(typeof(Match), "_awayScore", _match);
        _match.IncrementAway();
        int currentAway = (int)PrivateValueAccessor.GetPrivateFieldValue(typeof(Match), "_awayScore", _match);
        Assert.That(originalAway + 1, Is.EqualTo(currentAway));
    }
}
