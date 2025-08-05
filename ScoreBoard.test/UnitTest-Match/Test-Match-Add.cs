namespace ScoreBoard.test.UnitTest_Match;

[TestFixture]
public class UnitTestMatchScoreAdd
{
    private MatchTest _match;

    [SetUp]
    public void SetUp()
    {
        _match= new MatchTest("A","B");
    }
    
    [TestCase(0,0)]
    [TestCase(1,0)]
    [TestCase(2,0)]
    [TestCase(1,1)]
    [TestCase(2,2)]
    [TestCase(0,1)]
    [TestCase(0,2)]
    [TestCase(2,2)]
    public void MatchAdd_IsCorrectlyUpdated(int home, int away)
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
    public void MatchAdd_NoNegativeScoresNoNegativeScores(int home, int away)
    {
        Assert.Throws<Exception>(() => _match.Add(home, away));
    }
    
#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase(1,null)]
    [TestCase(null,1)]
    [TestCase(null,null)]
#pragma warning restore NUnit1001
    public void MatchAdd_ThrowsException_ProvidedScoreMustBeAnAbsoluteNumber(int home, int away)
    {
        Assert.Throws<Exception>(() => _match.Add(home, away));
    }
    
#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase("",1)]
    [TestCase(1,"")]
    [TestCase("","")]
    [TestCase(1.1,1)]
    [TestCase(1,1.1)]
    [TestCase(1.1,1.1)]
#pragma warning restore NUnit1001
    public void MatchAdd__Fuzz_ThrowsException_ProvidedScoreMustBeAnAbsoluteNumber(int home, int away)
    {
        Assert.Throws<Exception>(() => _match.Add(home, away));
    }
}