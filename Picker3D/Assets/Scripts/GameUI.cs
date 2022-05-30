using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentLevel, nextLevel, scoreText, subtitle;
    [SerializeField] private GameObject levelCompleted, levelFailed, gameInstructions;
    [SerializeField] private Image[] modulesStatus;
    private int _moduleIndex;
    private Timer _timer;
    private bool _levelCompleted;

    /// <summary>
    ///
    /// This script handling the game UI
    /// 
    /// </summary>
    
    private void Awake()
    {
        _timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        // Set current score and level in the beginning
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
    
    public void SetInstructionsOff() // Disable instructions when picker is moved
    {
        gameInstructions.SetActive(false);
    }

    #region Module Methods

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

    #endregion
    
    public void SetLevelCompleted() 
    {
       _levelCompleted = true;
        int gainedScore = ScoreCalculator.Instance.GetScore();
        
        // Set gained score, set level
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score", 0) + gainedScore);
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 0) + 1);
        
        SetScore();
        SetSubtitle("+" + gainedScore, 2f);
    }

    public void SetSubtitle(string text, float time)
    {
        _timer.OnTimeIsUp += TimerOnOnTimeIsUp;
        subtitle.text = text;
        _timer.SetTimer(time);
        _timer.StartTimer();
    }

    #region Event Handlers

    private void TimerOnOnTimeIsUp()
    {
        subtitle.text = "";
        if (_levelCompleted) // if level is completed next level panel is set
            levelCompleted.SetActive(true);
        _timer.OnTimeIsUp -= TimerOnOnTimeIsUp;
    }

    #endregion
    
    private void OnDisable()
    {
        if (_timer != null)
            _timer.OnTimeIsUp -= TimerOnOnTimeIsUp;
    }
}