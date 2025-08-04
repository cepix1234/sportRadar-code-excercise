using ScoreBoard.Interface;

namespace ScoreBoard.test;

public class MatchTest : Match
{
    public int _homeScore;
    public int _awayScore;
    public long _matchStart;
}

[TestFixture]
public class UnitTestMatch_ScoreUpdate
{
    private MatchTest _match;

    [SetUp]
    public void SetUp()
    {
        _match= new MatchTest();
    }

    [TestCase(1,0)]
    [TestCase(2,0)]
    [TestCase(0,1)]
    [TestCase(1,1)]
    [TestCase(0,2)]
    public void MatchUpdate_UpdateScore_IsCorrectlyUpdated(int home, int away)
    {
        _match.Update(home,away);
        Assert.Equals(_match._homeScore, home);
        Assert.Equals(_match._awayScore, away);
    }
    
    [TestCase(-1,0)]
    [TestCase(0,-1)]
    [TestCase(-1,-1)]
    public void MatchUpdate_UpdateScore_NoNegativeScores(int home, int away)
    {
        Assert.Throws<Exception>(() => _match.Update(home, away));
    }
    
    [TestCase(0,0)]
    [TestCase(1,0)]
    [TestCase(2,0)]
    [TestCase(1,1)]
    [TestCase(2,2)]
    [TestCase(0,1)]
    [TestCase(0,2)]
    [TestCase(2,2)]
    public void MatchUpdate_AddScore_IsCorrectlyUpdated(int home, int away)
    {
        int originalHome= _match._homeScore;
        int originalAway = _match._awayScore;
        _match.Add(home,away);
        Assert.Equals(_match._homeScore, originalHome + home);
        Assert.Equals(_match._awayScore, originalAway + away);
    }
    [TestCase(-1,0)]
    [TestCase(0,-1)]
    [TestCase(-1,-1)]
    public void MatchUpdate_AddScore_NoNegativeScores(int home, int away)
    {
        Assert.Throws<Exception>(() => _match.Add(home, away));
    }
    
    [Test]
    public void MatchUpdate_IncreaseHomeScore()
    {
        int originalHome = _match._homeScore;
        _match.IncrementHome();
        Assert.Equals(_match._homeScore, originalHome + 1);
    }
    
    [Test]
    public void MatchUpdate_IncreaseAwayScore()
    {
        int originalAway= _match._awayScore;
        _match.IncrementAway();
        Assert.Equals(_match._awayScore, originalAway+ 1);
    }
}

[TestFixture]
public class UnitTestMatch_CompareMatches
{
    private MatchTest _match1;
    private Match _match2;

    [SetUp]
    public void SetUp()
    {
        _match1= new MatchTest();
        _match2= new MatchTest();
    }

    [Test]
    public void MatchUpdate_UpdateScore_IsCorrectlyUpdated()
    {
        //TODO
    }
}