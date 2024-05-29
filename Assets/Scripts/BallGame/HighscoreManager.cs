using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager Instance;

    private const string HighscoreKey = "Highscore";

    public int Highscore { get; private set; }

    void Awake()
    {
        Instance = this;
        LoadHighscore();
    }

    public void LoadHighscore()
    {
        Highscore = PlayerPrefs.GetInt(HighscoreKey, 0);
    }

    public void SaveHighscore(int newHighscore)
    {
        if (newHighscore > Highscore)
        {
            Highscore = newHighscore;
            PlayerPrefs.SetInt(HighscoreKey, Highscore);
            PlayerPrefs.Save();
        }
    }

    public void ResetHighscore()
    {
        Highscore = 0;
        PlayerPrefs.SetInt(HighscoreKey, Highscore);
        PlayerPrefs.Save();
    }
}