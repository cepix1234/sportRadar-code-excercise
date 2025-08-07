using ScoreBoard.Exceptions;
using ScoreBoard.Interface;

namespace ScoreBoard;

public class Match : IMatch
{
    private readonly long _matchStart;
    private readonly string _homeTeamName = null!;
    private readonly string _awayTeamName = null!;

    private int _homeScore;
    private int _awayScore;
    public Match(string homeTeamName, string awayTeamName) : this(homeTeamName, awayTeamName, (0, 0)) { }

    public Match(string homeTeamName, string awayTeamName, (int home, int away) score)
    {
        this.HomeTeamName = homeTeamName;
        this.AwayTeamName = awayTeamName;
        this.HomeScore = score.home;
        this.AwayScore = score.away;
        this.MatchStart = DateTime.Now.Ticks;
    }

    public string HomeTeamName
    {
        private init { this.ValidateTeamName(value); this._homeTeamName = value; }
        get => this._homeTeamName;
    }

    public string AwayTeamName
    {
        private init { this.ValidateTeamName(value); this._awayTeamName = value; }
        get => this._awayTeamName;
    }

    public long MatchStart
    {
        private init => this._matchStart = value;
        get => this._matchStart;
    }

    private int HomeScore
    {
        set { this.ValidateMatchScore(value); this._homeScore = value; }
        get => this._homeScore;
    }

    private int AwayScore
    {
        set { this.ValidateMatchScore(value); this._awayScore = value; }
        get => this._awayScore;
    }

    public void Update(int home, int away)
    {
        this.ValidateMatchUpdate((home, away));
        this.HomeScore = home;
        this.AwayScore = away;
    }

    public void Add(int home, int away)
    {
        this.ValidateMatchScore(home);
        this.ValidateMatchScore(away);
        this.HomeScore += home;
        this.AwayScore += away;
    }

    public void IncrementHome()
    {
        this.Add(1, 0);
    }

    public void IncrementAway()
    {
        this.Add(0, 1);
    }

    public int Compare(IMatch match)
    {
        this.ValidateMatchCompare(match);
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
            return 1;
        }
        else if (this._matchStart > match.MatchStart)
        {
            return -1;
        }

        return String.Compare(this._homeTeamName, match.HomeTeamName, StringComparison.Ordinal);
    }

    public int ScoreSum()
    {
        return this._homeScore + this._awayScore;
    }

    public override string ToString()
    {
        return $"{this._homeTeamName}: {this._homeScore} - {this._awayTeamName}: {this._awayScore}";
    }

    private void ValidateTeamName(string teamName)
    {
        if (String.IsNullOrEmpty(teamName))
        {
            throw new MatchExceptions("Team name cannot be null or empty.");
        }

        if (teamName.GetType() != typeof(string))
        {
            throw new MatchExceptions("Team name not in correct format.");
        }

    }

    private void ValidateMatchScore(int score)
    {
        if (score.GetType() != typeof(int) || score < 0)
        {
            throw new MatchExceptions("Team score not in correct format.");
        }
    }

    private void ValidateMatchUpdate((int home, int away) score)
    {
        if (score.home < this._homeScore)
        {
            throw new MatchExceptions("Score can not reduce.");
        }
        if (score.away < this._awayScore)
        {
            throw new MatchExceptions("Score can not reduce.");
        }
    }

    private void ValidateMatchCompare(IMatch match)
    {
        if (!(match is Match))
        {
            throw new MatchExceptions("Match type not in correct format.");
        }
    }
}
