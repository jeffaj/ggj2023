
// score related things
public class ScoreTracker
{
    private int LastScoreSnapshot;
    public int CurrentScore { get; private set; }

    public void IncrementCurrentScore(int delta)
    {
        CurrentScore += delta;
    }

    // commit current score. use when going to next level.
    public void CommitCurrentScore()
    {
        LastScoreSnapshot = CurrentScore;
    }

    // reset score to last saved value. use when reseting level.
    public void ResetCurrentScore()
    {
        CurrentScore = LastScoreSnapshot;
    }
}