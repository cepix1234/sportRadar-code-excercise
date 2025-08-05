namespace ScoreBoard.test.UnitTest_Match;

[TestFixture]
public class UnitTestMatchScoreIncrement
{
    private MatchTest _match;

    [SetUp]
    public void SetUp()
    {
        _match= new MatchTest("A","B");
    }
    
    [Test]
    public void MatchIncrement_HomeScoreIsIncremented()
    {
        int originalHome = _match._homeScore;
        _match.IncrementHome();
        Assert.Equals(_match._homeScore, originalHome + 1);
    }
    
    [Test]
    public void MatchIncrement_AwayScoreIsIncremented()
    {
        int originalAway= _match._awayScore;
        _match.IncrementAway();
        Assert.Equals(_match._awayScore, originalAway+ 1);
    }
}