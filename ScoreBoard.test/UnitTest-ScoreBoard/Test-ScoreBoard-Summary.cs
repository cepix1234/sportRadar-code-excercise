using System.Reflection;
using ScoreBoard.Interface;

namespace ScoreBoard.test.UnitTest_ScoreBoard;

[TestFixture]
public class UnitTestScoreBoardSummary
{
    private ScoreBoardTest _scoreBoard;

    [SetUp]
    public void SetUp()
    {
        _scoreBoard = new ScoreBoardTest();
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
        IMatch matchOne = _scoreBoard._matches[FirstMatch];
        PropertyInfo propertyInfoOne = typeof(Match).GetProperty("_matchStart");
        IMatch matchTwo= _scoreBoard._matches[SecondMatch];
        PropertyInfo propertyInfoTwo= typeof(Match).GetProperty("_matchStart");
        propertyInfoOne.SetValue(matchOne, propertyInfoTwo.GetValue(matchTwo));
        
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