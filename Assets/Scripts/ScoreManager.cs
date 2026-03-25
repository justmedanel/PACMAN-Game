using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text highscoreText;
    private int highscore;

    public void SetScore(int score)
    {
        scoreText.text = $"{score} POINTS";
        if (score > highscore)
        {
            highscore = score;
            highscoreText.text = $"HIGHSCORE: {highscore}";
        }
    }
}
