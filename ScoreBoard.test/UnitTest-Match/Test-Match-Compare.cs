using ScoreBoard.Exceptions;

namespace ScoreBoard.test.UnitTest_Match;

[TestFixture]
public class UnitTestMatchCompareMatches
{
    private MatchTest _match1;
    private MatchTest _match2;

    [SetUp]
    public void SetUp()
    {
        _match1= new MatchTest("A","B");
        _match2= new MatchTest("C","D");
    }

    [Test]
    public void MatchCompare_MatchIsBefore_DifferentScore()
    {
        _match1.Update(2, 0);
        _match2.Update(1, 0);
        Assert.That(-1, Is.EqualTo(_match1.Compare(_match2)));
    }
    
    [Test]
    public void MatchCompare_MatchIsBefore_SameScore_DifInTime()
    {
        _match1.Update(2, 0);
        _match1._matchStart = 1;
        _match2.Update(2, 0);
        _match2._matchStart = 2;
        Assert.That(-1, Is.EqualTo(_match1.Compare(_match2)));
    }
    
    [Test]
    public void MatchCompare_MatchIsAfter_DifferentScore()
    {
        _match1.Update(1, 0);
        _match2.Update(2, 0);
        Assert.That(1, Is.EqualTo(_match1.Compare(_match2)));
    }
    
    [Test]
    public void MatchCompare_MatchIsAfter_SameScore_DiffInTime()
    {
        _match1.Update(2, 0);
        _match1._matchStart = 2;
        _match2.Update(2, 0);
        _match2._matchStart = 1;
        Assert.That(1, Is.EqualTo(_match1.Compare(_match2)));
    }
    
    [Test]
    public void MatchCompare_MatchIsBefore_SameScore_SameTime()
    {
        _match1.Update(2, 0);
        _match1._homeTeamName = "A";
        _match1._matchStart = 1;
        _match2.Update(2, 0);
        _match2._homeTeamName= "B";
        _match2._matchStart = 1;
        Assert.That(-1, Is.EqualTo(_match1.Compare(_match2)));
    }
    
    [Test]
    public void MatchCompare_MatchIsAfter_SameScore_SameTime()
    {
        _match1.Update(2, 0);
        _match1._homeTeamName = "B";
        _match1._matchStart = 1;
        _match2.Update(2, 0);
        _match2._homeTeamName= "A";
        _match2._matchStart = 1;
        Assert.That(1, Is.EqualTo(_match1.Compare(_match2)));
    }
    
    [Test]
    public void MatchCompare_throwsException_ProvidedMatchMustNotBeNull()
    {
        Assert.Throws<MatchExceptions>(() => _match1.Compare(null));
    }
}