using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI scoreText;
    public GameObject pauseGamePanel;
    public GameObject gameOverPanel;
    public GameObject ScorePanel;
    private int maxScore = 2;
    
    
    private int _score;
    public int Score
    {
        get => _score;
        set
        {
            if (_score != value)
            {
                _score = value;
                UpdateScoreUI();
            }
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Score = 0;
        UpdateScoreUI();
    }
    private void UpdateScoreUI()
    {
        scoreText.text = $"{Score} / {maxScore}";

        if (Score >= maxScore)
        {
            GameSceneManager.Instance.gameState = GameSceneManager.GameState.GameOver;
            gameOverPanel.SetActive(true);
            ScorePanel.SetActive(false);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
