using ScoreBoard.Exceptions;
using ScoreBoard.Interface;

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
    public Guid StartMatch(string homeTeam, string awayTeam)
    {
        return this.StartMatch(homeTeam, awayTeam,(0,0));
    }

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

    public Guid GetMatch(string teamName)
    {
        Validator.ValidateTeamName(teamName, TeamNameException);
        if (!this._teamNameMatches.TryGetValue(teamName, out Guid matchId))
        {
            throw new ScoreBoardException($"Match for team {teamName} not found");
        }
        return matchId;
    }

    public void UpdateMatch(Guid matchId, (int home, int away) score)
    {
        this.GetMatchById(matchId).Update(score.home, score.away);
    }

    public void UpdateMatch(string teamName, (int home, int away) score)
    {
       this.GetMatchByTeamName(teamName).Update(score.home, score.away);
    }

    public void AddScore(Guid matchId, (int home, int away) score)
    {
        this.GetMatchById(matchId).Add(score.home, score.away);
    }

    public void AddScore(string teamName, (int home, int away) score)
    {
        this.GetMatchByTeamName(teamName).Add(score.home, score.away);
    }

    public void IncrementHome(Guid matchId)
    {
        this.AddScore(matchId, (1,0));
    }

    public void IncrementHome(string teamName)
    {
        this.AddScore(teamName, (1,0));
    }

    public void IncrementAway(Guid matchId)
    {
        this.AddScore(matchId, (0,1));
    }

    public void IncrementAway(string teamName)
    {
        this.AddScore(teamName, (0,1));
    }

    public void FinishMatch(Guid matchId)
    {
        Validator.ValidateMatchId(matchId, MatchIdException);
        if(!this._matches.TryGetValue(matchId, out var match))
        {
            throw new ScoreBoardException($"Match {matchId} not found");
        }
        this._matches.Remove(matchId);
        this._teamNameMatches.Remove(match.HomeTeamName);
        this._teamNameMatches.Remove(match.AwayTeamName);
    }

    public void FinishMatch(string teamName)
    {
        Validator.ValidateTeamName(teamName, TeamNameException);
        
        if(!this._teamNameMatches.TryGetValue(teamName, out var matchId))
        {
            throw new ScoreBoardException($"Match for team {teamName} not found");
        }
        if(!this._matches.TryGetValue(matchId, out var match))
        {
            throw new ScoreBoardException($"Match {matchId} not found");
        }

        this._matches.Remove(matchId);
        this._teamNameMatches.Remove(match.HomeTeamName);
        this._teamNameMatches.Remove(match.AwayTeamName);
    }

    public string Summary()
    {
        IMatch[] Array_matches = this._matches.Values.ToArray();
        Array.Sort(Array_matches, (IMatch a, IMatch b) => a.Compare(b));

        return String.Join("\n",Array.ConvertAll(Array_matches, (IMatch a) => a.ToString()));
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
        if(!this._teamNameMatches.TryGetValue(teamName, out var matchId))
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
