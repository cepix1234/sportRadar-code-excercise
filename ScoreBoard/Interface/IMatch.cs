namespace ScoreBoard.Interface;

public interface IMatch
{
    /// <summary>
    /// Update the match score with the provided score.
    /// </summary>
    /// <param name="home">Home score to update the match with.</param>
    /// <param name="away">Away score to update the match with.</param>
    void Update(int home, int away);

    /// <summary>
    /// Add to the match score.
    /// </summary>
    /// <param name="home">Add to home score of the match.</param>
    /// <param name="away">Add to away score of the match</param>
    void Add(int home, int away);

    /// <summary>
    /// Increment home score by 1.
    /// </summary>
    void IncrementHome();

    /// <summary>
    /// Increment away score by 1.
    /// </summary>
    void IncrementAway();

    /// <summary>
    /// Compare current match with provided one.
    ///
    /// Check if the current score sum is greater or lower.
    /// If the score sum is equal check the start time if it is sooner or later.
    /// </summary>
    /// <param name="match">Match to check against.</param>
    /// <returns>-1 or 1 if the current match is to be shown before or after the provided match.</returns>
    int Compare(IMatch match);
}
