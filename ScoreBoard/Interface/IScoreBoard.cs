namespace ScoreBoard.Interface;

public interface IScoreBoard
{
    /// <summary>
    /// Start match between home and away team.
    /// 
    /// Set start score to default 0-0.
    /// </summary>
    /// <param name="homeTeam">Name of home team</param>
    /// <param name="awayTeam">Name of away team</param>
    /// <returns>Uniq identifier for the match for referencing.</returns>
    Guid StartMatch(string homeTeam, string awayTeam);

    /// <summary>
    /// Start match between home and away team.
    /// 
    /// Set the start score.
    /// </summary>
    /// <param name="homeTeam">Name of home team</param>
    /// <param name="awayTeam">Name of away team</param>
    /// <param name="score">Score to start the match at (e.g. (home:0, away:1))</param>
    /// <returns>Uniq identifier of the match for referencing.</returns>
    Guid StartMatch(string homeTeam, string awayTeam, (int home, int away) score);

    /// <summary>
    /// Get Match Guid by any team name.
    /// 
    /// </summary>
    /// <param name="teamName">Any team name</param>
    /// <returns>Guid of the match</returns>
    Guid GetMatch(string teamName);

    /// <summary>
    /// For the match with the provided id, update the score to the provided score.
    /// </summary>
    /// <param name="matchId">Uniq match identifier.</param>
    /// <param name="score">Math score to update to (e.g. (home:1,away:0))</param>
    void UpdateMatch(Guid matchId, (int home, int away) score);

    /// <summary>
    /// For the match with the provided team name, update the score to the provided score.
    /// </summary>
    /// <param name="teamName">Name of any team in the match to update the score of.</param>
    /// <param name="score">Math score to update to (e.g. (home:1,away:0))</param>

    void UpdateMatch(string teamName, (int home, int away) score);

    /// <summary>
    /// For the match with the provided id, increment the score with the provided score.
    ///
    /// e.g. if current match status is (home:2, away:0) and the provided score is (home:1, away:0) the match status will be (home:3, away:0)
    /// </summary>
    /// <param name="matchId">Uniq match identifier.</param>
    /// <param name="score">Math score to increment by(e.g. (home:1,away:0))</param>
    void AddScore(Guid matchId, (int home, int away) score);

    /// <summary>
    /// For the match with the provided team name, increment the score with the provided score.
    ///
    /// e.g. if current match status is (home:2, away:0) and the provided score is (home:1, away:0) the match status will be (home:3, away:0)
    /// </summary>
    /// <param name="teamName">Name of any team in the match to update add the score of.</param>
    /// <param name="score">Math score to increment by(e.g. (home:1,away:0))</param>
    void AddScore(string teamName, (int home, int away) score);

    /// <summary>
    /// For the match with the provided id, increment the home score by 1.
    /// </summary>
    /// <param name="matchId">Uniq match identifier.</param>
    void IncrementHome(Guid matchId);

    /// <summary>
    /// For the match with the provided team name, increment the home score by 1.
    /// </summary>
    /// <param name="teamName">Name of any team in the match to increment the home score.</param>
    void IncrementHome(string teamName);

    /// <summary>
    /// For the match with the provided id, increment the away score by 1.
    /// </summary>
    /// <param name="matchId">Uniq match identifier.</param>
    void IncrementAway(Guid matchId);

    /// <summary>
    /// For the match with the provided team name, increment the away score by 1.
    /// </summary>
    /// <param name="teamName">Name of any team in the match to increment the away score.</param>
    void IncrementAway(string teamName);

    /// <summary>
    /// Finish match with the provided id.
    /// </summary>
    /// <param name="matchId">Uniq match identifier.</param>
    void FinishMatch(Guid matchId);

    /// <summary>
    /// Finish match with the provided team name.
    /// </summary>
    /// <param name="matchId">Finish a match with includes the provided team name.</param>
    void FinishMatch(string teamName);

    /// <summary>
    /// Get all ongoing matches order by total score and duration.
    /// </summary>
    /// <returns>String table representation of ongoing matchers</returns>
    string Summary();
}
