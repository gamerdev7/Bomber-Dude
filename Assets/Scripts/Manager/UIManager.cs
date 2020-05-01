using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;

    private float totalTime = 200f;
    private float timeLeft;

    private void Start()
    {
        timeLeft = totalTime;

        scoreText.text = $"Score: {GameManager.Instance.Score.ToString("D3")}";
        timerText.text = $"Time: {timeLeft}";
    }

    private void Update()
    {
        UpdateTimer();
    }

    public void UpdateScore(int points)
    {
        GameManager.Instance.Score += points;
        scoreText.text = $"Score: {GameManager.Instance.Score}";
        Debug.Log("Score Updated");
    }

    private void UpdateTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            GameManager.Instance.CanMakeEnemiesFast = true;
        }

        timerText.text = $"Time: {timeLeft.ToString("F0")}";
    }
}
