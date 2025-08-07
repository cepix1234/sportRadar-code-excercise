using ScoreBoard.test.Test_Utils;

namespace ScoreBoard.test.UnitTest_Match;

[TestFixture]
public class UnitTestMatchToStringMatches
{
    private Match _match;

    [SetUp]
    public void SetUp()
    {
        _match = new Match("A", "B");
    }

    [Test]
    public void MatchToString_OutputIsCorrectlyFormated()
    {
        _match.Update(2, 0);
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_homeTeamName", _match, "A");
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_awayTeamName", _match, "B");
        Assert.That($"A: 2 - B: 0", Is.EqualTo(_match.ToString()));
    }
}
