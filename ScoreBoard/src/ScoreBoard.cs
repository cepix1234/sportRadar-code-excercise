using ScoreBoard.Exceptions;
using ScoreBoard.Interface;

namespace ScoreBoard;

public class ScoreBoard : IScoreBoard
{
    private Dictionary<Guid, IMatch> _matches;
    private Dictionary<string, Guid> _teamNameMatches;

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
        this.ValidateStartMatch(homeTeam, awayTeam);
        IMatch match  = new Match(homeTeam, awayTeam, score);
        Guid matchId = Guid.NewGuid();
        this._matches.Add(matchId, match);
        this._teamNameMatches.Add(homeTeam, matchId);
        this._teamNameMatches.Add(awayTeam, matchId);
        return matchId;
    }

    public Guid GetMatch(string teamName)
    {
        this.ValidateTeamName(teamName);
        return this._teamNameMatches[teamName];
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
        this.ValidateMatchId(matchId);
        IMatch match = this.GetMatchById(matchId);
        if (match == null)
        {
            throw new ScoreBoardException($"Match {matchId} not found");
        }
        this._matches.Remove(matchId);
        this._teamNameMatches.Remove(match.HomeTeamName);
        this._teamNameMatches.Remove(match.AwayTeamName);
    }

    public void FinishMatch(string teamName)
    {
        this.ValidateTeamName(teamName);
        Guid matchId = this._teamNameMatches[teamName];
        if (matchId == Guid.Empty)
        {
            throw new ScoreBoardException($"Match for team {teamName} not found");
        }
        IMatch match = this._matches[matchId];
        if (match == null)
        {
            throw new ScoreBoardException($"Match {matchId} not found");
        }

        this._matches.Remove(matchId);
        this._teamNameMatches.Remove(match.HomeTeamName);
        this._teamNameMatches.Remove(match.AwayTeamName);
    }

    public string Summary()
    {
        
        throw new NotImplementedException();
    }

    private IMatch GetMatchById(Guid matchId)
    {
        this.ValidateMatchId(matchId);
        IMatch match = this._matches[matchId];
        if (match == null)
        {
            throw new ScoreBoardException($"Match {matchId} not found");
        }
        return match;
    }
    
    private IMatch GetMatchByTeamName(string teamName)
    {
        this.ValidateTeamName(teamName);
        Guid matchId = this._teamNameMatches[teamName];
        if (matchId == Guid.Empty)
        {
            throw new ScoreBoardException($"Match for team {teamName} not found");
        }
        IMatch match = this._matches[matchId];
        if (match == null)
        {
            throw new ScoreBoardException($"Match {matchId} not found");
        }
        return match;
    }

    private void ValidateStartMatch(string homeTeam, string awayTeam)
    {
        if (homeTeam == awayTeam)
        {
            throw new ScoreBoardException("Team names must be different.");
        }
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
    
    private void ValidateMatchId(Guid matchId)
    {
        if (matchId.GetType() != typeof(Guid))
        {
            throw new ScoreBoardException("Match id not in correct format.");
        }
    }
}
