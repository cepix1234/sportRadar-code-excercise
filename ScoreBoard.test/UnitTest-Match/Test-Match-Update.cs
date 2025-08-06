using ScoreBoard.Exceptions;

namespace ScoreBoard.test.UnitTest_Match;

[TestFixture]
public class UnitTestMatchScoreUpdate
{
    private MatchTest _match;

    [SetUp]
    public void SetUp()
    {
        _match= new MatchTest("A","B");
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
    public void MatchUpdate_UpdateScore_ThrowsException_ProvidedScoreMustBeAnAbsoluteNumber(int home, int away)
    {
        Assert.Throws<MatchExceptions>(() => _match.Update(home, away));
    }
    
#pragma warning disable NUnit1001 // Creation of Match should check the arguments are set correctly.
    [TestCase("",1)]
    [TestCase(1,"")]
    [TestCase("","")]
    [TestCase(1.1,1)]
    [TestCase(1,1.1)]
    [TestCase(1.1,1.1)]
#pragma warning restore NUnit1001
    public void MatchUpdate_UpdateScore_Fuzz_ThrowsException_ProvidedScoreMustBeAnAbsoluteNumber(int home, int away)
    {
        Assert.Throws<MatchExceptions>(() => _match.Update(home, away));
    }
}