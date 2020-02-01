
public class GameManager
{
    private static GameManager _instance;

    private long _score;

    public readonly float TimePerLevel = 59;

    public long Score => _score;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }

            return _instance;
        }
    }

    public long AddScore(long scoreToAdd)
    {
        return _score += scoreToAdd;
    }
}
