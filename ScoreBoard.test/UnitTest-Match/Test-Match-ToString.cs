namespace ScoreBoard.test.UnitTest_Match;

[TestFixture]
public class UnitTestMatchToStringMatches
{
    private MatchTest _match;

    [SetUp]
    public void SetUp()
    {
        _match= new MatchTest("A","B");
    }

    [Test]
    public void MatchCompare_MatchIsBefore_DifferentScore()
    {
        _match.Update(2, 0);
        _match._homeTeamName = "A";
        _match._awayTeamName = "B";
        Assert.Equals(_match.ToString(), $"A: 2 - B: 0");
    }
}