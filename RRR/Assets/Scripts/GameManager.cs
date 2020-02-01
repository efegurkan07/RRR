
public class GameManager
{
    private static GameManager _instance;

    private long _score;

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
