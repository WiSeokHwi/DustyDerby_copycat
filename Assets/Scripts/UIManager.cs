using System;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI scoreText;
    private int maxScore = 10;
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
    }
}
