using ScoreBoard.Exceptions;
using ScoreBoard.test.Test_Utils;

namespace ScoreBoard.test.UnitTest_Match;

[TestFixture]
public class UnitTestMatchCompareMatches
{
    private Match _match1;
    private Match _match2;

    [SetUp]
    public void SetUp()
    {
        _match1 = new Match("A", "B");
        _match2 = new Match("C", "D");
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
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", _match1, 2);
        _match2.Update(2, 0);
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", _match2, 1);
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
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", _match1, 1);
        _match2.Update(2, 0);
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", _match2, 2);
        Assert.That(_match1.Compare(_match2), Is.EqualTo(1));
    }

    [Test]
    public void MatchCompare_MatchIsBefore_SameScore_SameTime()
    {
        _match1.Update(2, 0);
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_homeTeamName", _match1, "A");
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", _match1, 1);
        _match2.Update(2, 0);
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_homeTeamName", _match2, "B");
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", _match2, 1);
        Assert.That(-1, Is.EqualTo(_match1.Compare(_match2)));
    }

    [Test]
    public void MatchCompare_MatchIsAfter_SameScore_SameTime()
    {
        _match1.Update(2, 0);
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_homeTeamName", _match1, "B");
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", _match1, 1);
        _match2.Update(2, 0);
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_homeTeamName", _match2, "A");
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", _match2, 1);
        Assert.That(1, Is.EqualTo(_match1.Compare(_match2)));
    }

    [Test]
    public void MatchCompare_throwsException_ProvidedMatchMustNotBeNull()
    {
        Assert.Throws<MatchExceptions>(() => _match1.Compare(null));
    }
}
