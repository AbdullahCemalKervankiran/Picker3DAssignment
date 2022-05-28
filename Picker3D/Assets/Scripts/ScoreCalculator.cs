using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    public static ScoreCalculator Instance;
    [SerializeField] private TextMeshPro[] scoreTexts;
    private int _scoreIndex = 0;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        
    }
    
    public void NextScore()
    {
        _scoreIndex++;
    }

    public int GetScore()
    {
        return int.Parse(scoreTexts[_scoreIndex].text);
    }
}
