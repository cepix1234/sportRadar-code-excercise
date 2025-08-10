using ScoreBoard.Exceptions;
using ScoreBoard.Interface;
using ScoreBoard.Validate;

namespace ScoreBoard;

public class ScoreBoard : IScoreBoard
{
    private Dictionary<Guid, IMatch> _matches;
    private Dictionary<string, Guid> _teamNameMatches;

    private static readonly ScoreBoardException TeamNameException = new("Team name not in correct format.");
    private static readonly ScoreBoardException MatchIdException = new("Match id not in correct format.");

    public ScoreBoard()
    {
        this._matches = new Dictionary<Guid, IMatch>();
        this._teamNameMatches = new Dictionary<string, Guid>();
    }

    /// <summary>
    /// Start match between home and away team.
    /// 
    /// Set start score to default 0-0.
    /// </summary>
    /// <param name="homeTeam">Name of home team</param>
    /// <param name="awayTeam">Name of away team</param>
    // <returns>Uniq identifier for the match for referencing.</returns>
    public Guid StartMatch(string homeTeam, string awayTeam)
    {
        return this.StartMatch(homeTeam, awayTeam, (0, 0));
    }

    /// <summary>
    /// Start match between home and away team.
    /// 
    /// Set the start score.
    /// </summary>
    /// <param name="homeTeam">Name of home team</param>
    /// <param name="awayTeam">Name of away team</param>
    /// <param name="score">Score to start the match at (e.g. (home:0, away:1))</param>
    /// <returns>Uniq identifier of the match for referencing.</returns>
    public Guid StartMatch(string homeTeam, string awayTeam, (int home, int away) score)
    {
        IMatch match = new Match(homeTeam, awayTeam, score);
        if (this._teamNameMatches.ContainsKey(homeTeam) || this._teamNameMatches.ContainsKey(awayTeam))
        {
            throw new ScoreBoardException("Match with this team already exists.");
        }

        Guid matchId = Guid.NewGuid();
        this._matches.Add(matchId, match);
        this._teamNameMatches.Add(homeTeam, matchId);
        this._teamNameMatches.Add(awayTeam, matchId);
        return matchId;
    }

    /// <summary>
    /// Get Match Guid by any team name.
    /// 
    /// </summary>
    /// <param name="teamName">Any team name</param>
    /// <returns>Guid of the match</returns>
    public Guid GetMatch(string teamName)
    {
        Validator.ValidateTeamName(teamName, TeamNameException);
        if (!this._teamNameMatches.TryGetValue(teamName, out Guid matchId))
        {
            throw new ScoreBoardException($"Match for team {teamName} not found");
        }
        return matchId;
    }

    /// <summary>
    /// For the match with the provided id, update the score to the provided score.
    /// </summary>
    /// <param name="matchId">Uniq match identifier.</param>
    /// <param name="score">Math score to update to (e.g. (home:1,away:0))</param>
    public void UpdateMatch(Guid matchId, (int home, int away) score)
    {
        this.GetMatchById(matchId).Update(score.home, score.away);
    }

    /// <summary>
    /// For the match with the provided team name, update the score to the provided score.
    /// </summary>
    /// <param name="teamName">Name of any team in the match to update the score of.</param>
    /// <param name="score">Math score to update to (e.g. (home:1,away:0))</param>
    public void UpdateMatch(string teamName, (int home, int away) score)
    {
        this.GetMatchByTeamName(teamName).Update(score.home, score.away);
    }

    /// <summary>
    /// For the match with the provided id, increment the score with the provided score.
    ///
    /// e.g. if current match status is (home:2, away:0) and the provided score is (home:1, away:0) the match status will be (home:3, away:0)
    /// </summary>
    /// <param name="matchId">Uniq match identifier.</param>
    /// <param name="score">Math score to increment by(e.g. (home:1,away:0))</param>
    public void AddScore(Guid matchId, (int home, int away) score)
    {
        this.GetMatchById(matchId).Add(score.home, score.away);
    }

    /// <summary>
    /// For the match with the provided team name, increment the score with the provided score.
    ///
    /// e.g. if current match status is (home:2, away:0) and the provided score is (home:1, away:0) the match status will be (home:3, away:0)
    /// </summary>
    /// <param name="teamName">Name of any team in the match to update add the score of.</param>
    /// <param name="score">Math score to increment by(e.g. (home:1,away:0))</param>
    public void AddScore(string teamName, (int home, int away) score)
    {
        this.GetMatchByTeamName(teamName).Add(score.home, score.away);
    }

    /// <summary>
    /// For the match with the provided id, increment the home score by 1.
    /// </summary>
    /// <param name="matchId">Uniq match identifier.</param>
    public void IncrementHome(Guid matchId)
    {
        this.AddScore(matchId, (1, 0));
    }

    /// <summary>
    /// For the match with the provided team name, increment the home score by 1.
    /// </summary>
    /// <param name="teamName">Name of any team in the match to increment the home score.</param>
    public void IncrementHome(string teamName)
    {
        this.AddScore(teamName, (1, 0));
    }

    /// <summary>
    /// For the match with the provided id, increment the away score by 1.
    /// </summary>
    /// <param name="matchId">Uniq match identifier.</param>
    public void IncrementAway(Guid matchId)
    {
        this.AddScore(matchId, (0, 1));
    }

    /// <summary>
    /// For the match with the provided team name, increment the away score by 1.
    /// </summary>
    /// <param name="teamName">Name of any team in the match to increment the away score.</param>
    public void IncrementAway(string teamName)
    {
        this.AddScore(teamName, (0, 1));
    }

    /// <summary>
    /// Finish match with the provided id.
    /// </summary>
    /// <param name="matchId">Uniq match identifier.</param>
    public void FinishMatch(Guid matchId)
    {
        Validator.ValidateMatchId(matchId, MatchIdException);
        if (!this._matches.TryGetValue(matchId, out var match))
        {
            throw new ScoreBoardException($"Match {matchId} not found");
        }
        this._matches.Remove(matchId);
        this._teamNameMatches.Remove(match.HomeTeamName);
        this._teamNameMatches.Remove(match.AwayTeamName);
    }

    /// <summary>
    /// Finish match with the provided team name.
    /// </summary>
    /// <param name="matchId">Finish a match with includes the provided team name.</param>
    public void FinishMatch(string teamName)
    {
        Validator.ValidateTeamName(teamName, TeamNameException);

        if (!this._teamNameMatches.TryGetValue(teamName, out var matchId))
        {
            throw new ScoreBoardException($"Match for team {teamName} not found");
        }
        if (!this._matches.TryGetValue(matchId, out var match))
        {
            throw new ScoreBoardException($"Match {matchId} not found");
        }

        this._matches.Remove(matchId);
        this._teamNameMatches.Remove(match.HomeTeamName);
        this._teamNameMatches.Remove(match.AwayTeamName);
    }

    /// <summary>
    /// Get all ongoing matches order by total score and duration.
    /// </summary>
    /// <returns>String table representation of ongoing matches</returns>
    public string Summary()
    {
        IMatch[] Array_matches = this._matches.Values.ToArray();
        Array.Sort(Array_matches, (IMatch a, IMatch b) => a.Compare(b));

        return String.Join("\n", Array.ConvertAll(Array_matches, (IMatch a) => a.ToString()));
    }

    private IMatch GetMatchById(Guid matchId)
    {
        Validator.ValidateMatchId(matchId, MatchIdException);
        if (!this._matches.TryGetValue(matchId, out var match))
        {
            throw new ScoreBoardException($"Match {matchId} not found");
        }

        return match;
    }

    private IMatch GetMatchByTeamName(string teamName)
    {
        Validator.ValidateTeamName(teamName, TeamNameException);
        if (!this._teamNameMatches.TryGetValue(teamName, out var matchId))
        {
            throw new ScoreBoardException($"Match for team {teamName} not found");
        }
        if (!this._matches.TryGetValue(matchId, out var match))
        {
            throw new ScoreBoardException($"Match {matchId} not found");
        }
        return match;
    }
}
