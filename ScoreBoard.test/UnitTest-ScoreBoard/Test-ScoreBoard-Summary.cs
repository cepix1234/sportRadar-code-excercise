using System.Reflection;
using ScoreBoard.Interface;
using ScoreBoard.test.Test_Utils;

namespace ScoreBoard.test.UnitTest_ScoreBoard;

[TestFixture]
public class UnitTestScoreBoardSummary
{
    private ScoreBoard _scoreBoard;

    [SetUp]
    public void SetUp()
    {
        _scoreBoard = new ScoreBoard();
    }

    [Test]
    public void Summary_Scenario_NormalMatchesWithDifferentScoreSums()
    {
        _scoreBoard.StartMatch("Slovenia", "Croatia");
        _scoreBoard.StartMatch("Austria", "Denmark");
        _scoreBoard.StartMatch("Germany", "Spain");
        _scoreBoard.UpdateMatch("Slovenia", (1,0));
        _scoreBoard.UpdateMatch("Austria", (0,2));
        _scoreBoard.UpdateMatch("Germany", (1,2));
        string result = _scoreBoard.Summary();
        string[] expected = new string[] { "Germany: 1 - Spain: 2", "Austria: 0 - Denmark: 2", "Slovenia: 1 - Croatia: 0"};
        Assert.That(result, Is.EqualTo(String.Join("\n", expected)));
    }
    
    [Test]
    public void Summary_Scenario_2MatchesWithTheSameScoreSum()
    {
        _scoreBoard.StartMatch("Slovenia", "Croatia");
        _scoreBoard.StartMatch("Austria", "Denmark");
        _scoreBoard.StartMatch("Germany", "Spain");
        _scoreBoard.UpdateMatch("Slovenia", (1,0));
        _scoreBoard.UpdateMatch("Austria", (1,2));
        _scoreBoard.UpdateMatch("Germany", (1,2));
        string result = _scoreBoard.Summary();
        string[] expected = new string[] { "Austria: 1 - Denmark: 2", "Germany: 1 - Spain: 2", "Slovenia: 1 - Croatia: 0"};
        Assert.That(result, Is.EqualTo(String.Join("\n", expected)));
    }
    
    [Test]
    public void Summary_Scenario_2MatchesWithTheSameScoreSumAndSameMatchStartTime()
    {
        Guid FirstMatch = _scoreBoard.StartMatch("Slovenia", "Croatia");
        Guid SecondMatch = _scoreBoard.StartMatch("Austria", "Denmark");
        _scoreBoard.StartMatch("Germany", "Spain");
        _scoreBoard.UpdateMatch("Slovenia", (1,2));
        _scoreBoard.UpdateMatch("Austria", (1,2));
        _scoreBoard.UpdateMatch("Germany", (1,0));
       
        // Alter the times for scenario where 2 matches start at the same time.
        Dictionary<Guid, IMatch> matches = (Dictionary<Guid,IMatch>)PrivateValueAccessor.GetPrivateFieldValue(typeof(ScoreBoard), "_matches", _scoreBoard);
        IMatch matchOne = matches[FirstMatch];
        IMatch matchTwo= matches[SecondMatch];
        long toSetTime = (long)PrivateValueAccessor.GetPrivateFieldValue(typeof(Match), "_matchStart",matchTwo);
        PrivateValueAccessor.SetPrivateFieldValue(typeof(Match), "_matchStart", matchOne ,toSetTime);
        
        string result = _scoreBoard.Summary();
        string[] expected = new string[] { "Austria: 1 - Denmark: 2", "Slovenia: 1 - Croatia: 2", "Germany: 1 - Spain: 0"};
        Assert.That(result, Is.EqualTo(String.Join("\n", expected)));
    }

    [Test]
    public void Summary_Scenario_3MatchesWithDifferentScoresAndOneFinished()
    {
        _scoreBoard.StartMatch("Slovenia", "Croatia");
        _scoreBoard.StartMatch("Austria", "Denmark");
        _scoreBoard.StartMatch("Germany", "Spain");
        _scoreBoard.UpdateMatch("Slovenia", (1, 0));
        _scoreBoard.UpdateMatch("Austria", (1, 2));
        _scoreBoard.UpdateMatch("Germany", (1, 2));
        _scoreBoard.FinishMatch("Germany");
        string result = _scoreBoard.Summary();
        string[] expected = new string[]
            { "Austria: 1 - Denmark: 2", "Slovenia: 1 - Croatia: 0" };
        Assert.That(result, Is.EqualTo(String.Join("\n", expected)));
    }
}