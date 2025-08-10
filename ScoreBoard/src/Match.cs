using ScoreBoard.Exceptions;
using ScoreBoard.Interface;
using ScoreBoard.Validate;

namespace ScoreBoard;

public class Match : IMatch
{
    private readonly long _matchStart;
    private readonly string _homeTeamName = null!;
    private readonly string _awayTeamName = null!;

    private int _homeScore;
    private int _awayScore;

    private static readonly MatchException TeamNameException = new("Team name not in correct format.");
    private static readonly MatchException ScoreException = new("Score is not in correct format.");
    private static readonly MatchException MatchUpdateException = new("Score can not be reduced.");
    private static readonly MatchException MatchCompareException = new("Match type not in correct format.");
    private static readonly MatchException SameTeamNameException = new("Team names must be different.");

    public Match(string homeTeamName, string awayTeamName) : this(homeTeamName, awayTeamName, (0, 0)) { }

    public Match(string homeTeamName, string awayTeamName, (int home, int away) score)
    {
        Validator.ValidateSameTeamName(homeTeamName, awayTeamName, SameTeamNameException);
        this.HomeTeamName = homeTeamName;
        this.AwayTeamName = awayTeamName;
        this.HomeScore = score.home;
        this.AwayScore = score.away;
        this.MatchStart = DateTime.Now.Ticks;
    }

    /// <summary>
    /// Get home team name.
    /// </summary>
    public string HomeTeamName
    {
        private init { Validator.ValidateTeamName(value, TeamNameException); this._homeTeamName = value; }
        get => this._homeTeamName;
    }

    /// <summary>
    /// Get away team name.
    /// </summary>
    public string AwayTeamName
    {
        private init { Validator.ValidateTeamName(value, TeamNameException); this._awayTeamName = value; }
        get => this._awayTeamName;
    }

    /// <summary>
    /// Get match start time.
    /// </summary>
    public long MatchStart
    {
        private init => this._matchStart = value;
        get => this._matchStart;
    }

    /// <summary>
    /// Update the match score with the provided score.
    /// </summary>
    /// <param name="home">Home score to update the match with.</param>
    /// <param name="away">Away score to update the match with.</param>
    public void Update(int home, int away)
    {
        Validator.ValidateMatchUpdate((this.HomeScore, this.AwayScore), (home, away), MatchUpdateException);
        this.HomeScore = home;
        this.AwayScore = away;
    }

    /// <summary>
    /// Add to the match score.
    /// </summary>
    /// <param name="home">Add to home score of the match.</param>
    /// <param name="away">Add to away score of the match</param>
    public void Add(int home, int away)
    {
        Validator.ValidateMatchScore(home, TeamNameException);
        Validator.ValidateMatchScore(away, TeamNameException);
        this.HomeScore += home;
        this.AwayScore += away;
    }

    /// <summary>
    /// Increment home score by 1.
    /// </summary>
    public void IncrementHome()
    {
        this.Add(1, 0);
    }

    /// <summary>
    /// Increment away score by 1.
    /// </summary>
    public void IncrementAway()
    {
        this.Add(0, 1);
    }

    /// <summary>
    /// Compare current match with provided one.
    ///
    /// Check if the current score sum is greater or lower.
    /// If the score sum is equal check the start time if it is sooner or later.
    /// </summary>
    /// <param name="match">Match to check against.</param>
    /// <returns>-1 or 1 if the current match is to be shown before or after the provided match.</returns>
    public int Compare(IMatch match)
    {
        Validator.ValidateMatchCompare(match, MatchCompareException);
        if (this.ScoreSum() < match.ScoreSum())
        {
            return 1;
        }
        else if (this.ScoreSum() > match.ScoreSum())
        {
            return -1;
        }

        if (this._matchStart < match.MatchStart)
        {
            return -1;
        }
        else if (this._matchStart > match.MatchStart)
        {
            return 1;
        }

        return String.Compare(this._homeTeamName, match.HomeTeamName, StringComparison.Ordinal);
    }

    // <summary>
    /// Get match score sum.
    /// </summary>
    /// <returns>Sum of the current match score</returns>
    public int ScoreSum()
    {
        return this._homeScore + this._awayScore;
    }

    public override string ToString()
    {
        return $"{this._homeTeamName}: {this._homeScore} - {this._awayTeamName}: {this._awayScore}";
    }

    private int HomeScore
    {
        set { Validator.ValidateMatchScore(value, ScoreException); this._homeScore = value; }
        get => this._homeScore;
    }

    private int AwayScore
    {
        set { Validator.ValidateMatchScore(value, ScoreException); this._awayScore = value; }
        get => this._awayScore;
    }
}
