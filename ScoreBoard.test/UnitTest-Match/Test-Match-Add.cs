using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using ScoreBoard.Exceptions;
using ScoreBoard.test.Test_Utils;

namespace ScoreBoard.test.UnitTest_Match;

[TestFixture]
public class UnitTestMatchScoreAdd
{
    private Match _match;

    [SetUp]
    public void SetUp()
    {
        _match = new Match("A", "B");
    }

    [TestCase(0, 0)]
    [TestCase(1, 0)]
    [TestCase(2, 0)]
    [TestCase(1, 1)]
    [TestCase(2, 2)]
    [TestCase(0, 1)]
    [TestCase(0, 2)]
    [TestCase(2, 2)]
    public void MatchAdd_IsCorrectlyUpdated(int home, int away)
    {
        int originalHome = (int)PrivateValueAccessor.GetPrivateFieldValue(typeof(Match), "_homeScore", _match);
        int originalAway = (int)PrivateValueAccessor.GetPrivateFieldValue(typeof(Match), "_awayScore", _match);
        _match.Add(home, away);

        int currentHome = (int)PrivateValueAccessor.GetPrivateFieldValue(typeof(Match), "_homeScore", _match);
        int currentAway = (int)PrivateValueAccessor.GetPrivateFieldValue(typeof(Match), "_awayScore", _match);
        Assert.That(originalHome + home, Is.EqualTo(currentHome));
        Assert.That(originalAway + away, Is.EqualTo(currentAway));
    }

    [TestCase(-1, 0)]
    [TestCase(0, -1)]
    [TestCase(-1, -1)]
    public void MatchAdd_NoNegativeScoresNoNegativeScores(int home, int away)
    {
        Assert.Throws<MatchExceptions>(() => _match.Add(home, away));
    }
}
