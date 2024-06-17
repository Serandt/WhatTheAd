using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager Instance;

    private const string HighscoreKey = "Highscore";

    public float Highscore { get; private set; }

    void Awake()
    {
        Instance = this;
        LoadHighscore();
    }

    public void LoadHighscore()
    {

        Highscore = PlayerPrefs.GetFloat(HighscoreKey, 0);
    }

    public void SaveHighscore(float newHighscore)
    {
        if (newHighscore > Highscore)
        {
            Highscore = newHighscore;
            PlayerPrefs.SetFloat(HighscoreKey, (float)Highscore);
            PlayerPrefs.Save();
        }
    }

    public void ResetHighscore()
    {
        Highscore = 0;
        PlayerPrefs.SetFloat(HighscoreKey, (float)Highscore);
        PlayerPrefs.Save();
    }
}