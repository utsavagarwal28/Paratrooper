using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI hiScoreText;

    private int score = 0;
    private int level = 1;
    private int hiScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        if (score > hiScore)
            hiScore = score;
        UpdateUI();
    }

    public void IncreaseLevel()
    {
        level++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"Score: {score}";
        levelText.text = $"Level: {level}";
        hiScoreText.text = $"Hi-Score: {hiScore}";
    }
}
