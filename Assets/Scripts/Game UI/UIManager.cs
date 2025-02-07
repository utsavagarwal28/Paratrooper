using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI hiScoreText;

    public GameObject gameOverPanel;
    public GameObject restartButton;
    public GameObject quitButton;

    private int score = 0;
    private int level = 1;
    private int hiScore = 0;
    private int scoreToNextLevel = 500;

    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
        gameOverPanel.SetActive(false);
        UpdateUI();
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        if (score > hiScore)
            hiScore = score;

        if (score >= scoreToNextLevel)
        {
            IncreaseLevel();
        }

        UpdateUI();
    }

    public void IncreaseLevel()
    {
        level++;
        scoreToNextLevel += 500;
        levelManager.AdjustDifficulty(level);
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        levelText.text = $"Level: {level}";
        hiScoreText.text = $"Hi-Score: {hiScore}";
    }

    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        score = 0;
        level = 1;
        scoreToNextLevel = 500;
        levelManager.AdjustDifficulty(level);
        UpdateUI();
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
