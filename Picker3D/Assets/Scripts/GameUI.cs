using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentLevel, nextLevel, scoreText, subtitle;
    [SerializeField] private GameObject levelCompleted, levelFailed;
    [SerializeField] private Image[] modulesStatus;
    private int _moduleIndex = 0;
    private Timer _timer;
    private bool _levelCompleted;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        SetScore();
        SetLevelText();
    }

    private void SetLevelText()
    {
        currentLevel.text = "" + PlayerPrefs.GetInt("level", 1);
        nextLevel.text = "" + (PlayerPrefs.GetInt("level", 1) + 1);
    }

    private void SetScore()
    {
        scoreText.text = "" + PlayerPrefs.GetInt("score", 0);
    }

    public void SetModuleOK()
    {
        modulesStatus[_moduleIndex].color = Color.green;
        _moduleIndex++;
    }

    public void SetModuleFailed()
    {
        modulesStatus[_moduleIndex].color = Color.red;
        levelFailed.SetActive(true);
    }

    public void SetLevelCompleted()
    {
        Debug.Log("Level Completed");
        _levelCompleted = true;
        int gainedScore = ScoreCalculator.Instance.GetScore();
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score", 0) + gainedScore);
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 0) + 1);
        SetScore();
        SetSubtitle("+" + gainedScore, 2f);
        //levelCompleted.SetActive(true);
    }

    public void SetSubtitle(string text, float time)
    {
        _timer.OnTimeIsUp += TimerOnOnTimeIsUp;
        subtitle.text = text;
        _timer.SetTimer(time);
        _timer.StartTimer();
    }

    private void TimerOnOnTimeIsUp()
    {
        subtitle.text = "";
        if (_levelCompleted)
            levelCompleted.SetActive(true);
        _timer.OnTimeIsUp -= TimerOnOnTimeIsUp;
    }

    private void OnDisable()
    {
        if (_timer != null)
            _timer.OnTimeIsUp -= TimerOnOnTimeIsUp;
    }
}