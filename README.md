# sportRadar-code-excercise

Code excercise for sport radar

## Excersise description

Task requirements:
You are working in a sports data company, and we would like you to develop a new Live Football
World Cup Scoreboard library that shows all the ongoing matches and their scores.
The scoreboard supports the following operations:

1. Start a new match, assuming initial score 0 – 0 and adding it the scoreboard.
   This should capture following parameters:
   a. Home team
   b. Away team
2. Update score. This should receive a pair of absolute scores: home team score and away
   team score.
3. Finish match currently in progress. This removes a match from the scoreboard.
4. Get a summary of matches in progress ordered by their total score. The matches with the
   same total score will be returned ordered by the most recently started match in the
   scoreboard.
   For example, if following matches are started in the specified order and their scores
   respectively updated:
   a. Mexico 0 - Canada
   b. Spain 10 - Brazil 2
   c. Germany 2 - France 2
   d. Uruguay 6 - Italy 6
   e. Argentina 3 - Australia 1
   The summary should be as follows:
5. Uruguay 6 - Italy 6
6. Spain 10 - Brazil 2
7. Mexico 0 - Canada 5
8. Argentina 3 - Australia 1
9. Germany 2 - France 2

## Design

## Constraints:

1. 1 team can only attend 1 match at a time.
2. Match start will be when a match is added to the score board (not when the match started in the past).

## Additional prerequisites

1. When providing summary if two matches have the same score and the same time of start order by home team names.
2. The implementation does not specify the use of the library so we can expect malformed inputs.
3. The description does not specify in what kind of flow it will be used we can expect, a request will be made to get an existing match id.

## Score Board

### interface:

- StartMatch(string homeTeam, string awayTeam); Initial requirement start match.
- StartMatch(string homeTeam, string awayTeam, (int home, int away) score); Additional requirement start match with a different starting score.
- GetMatch(string teamName); Additional requirement get guid of match by providing any team name.
- UpdateMatch(Guid matchId, (int home, int away) score); Initial requirement update match score by providing match ID.
- UpdateMatch(string teamName, (int home, int away) score); Additional requirement update match score by providing any team name.
- AddScore(Guid matchId, (int home, int away) score); Additional requirement add score by tuple, with provided match ID.
- AddScore(string teamName, (int home, int away) score); Additional requirement add score by tuple, with provided any team name.
- IncrementHome(Guid matchId); Additional requirement increase home score by 1 with provided match ID.
- IncrementHome(string teamName); Additional requirement increase home score by 1 with provided any team name.
- IncrementAway(Guid matchId); Additional requirement increase away score by 1 with provided match ID.
- IncrementAway(string teamName); Additional requirement increase away score by 1 with provided any team name.
- FinishMatch(Guid matchId); Initial requirement finish a match by match ID.
- FinishMatch(string teamName); Initial requirement finish a match by any team name.
- Summary(); Initial requirement summarise tracked matches.

### implementation:

- Start match:

  Map of matches where the key is a GUID. Match is an implementation of [Match](#match)
  On new match check if a match with the same countries is already in the score board.
  Save the match score and return the GUID for best later referencing.

- Update match:

  Get the match by GUID or any team name.
  Update whole score, or add score, or increment score.

- Finish match:

  Remove match from map referenced by GUID of any team name.

- Summary

  Get all active matches.
  Order active matches by start of match and then by sum of scores.
  Format matches to string.

## Match

### interface:

- Update(int home, int away); Update the match score with the provided score.
- Add(int home, int away); Add the provided score to the current score.
- IncrementHome(); Increment home score by 1.
- IncrementAway(); Increment away score by 1.
- Compare(IMatch match); Compare with the provided mach first by score if the same then by time.
- ToString(); Return string representation of the match in format `{homeTeamName}:{homeTeamScore} - {awayTeamName}:{awayTeamScore}`
- ScoreSum(); Get the current sum of match score.
