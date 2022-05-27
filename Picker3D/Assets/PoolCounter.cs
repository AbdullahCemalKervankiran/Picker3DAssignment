using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoolCounter : MonoBehaviour
{
    public event Action OnEnoughBalls;
    public event Action OnNotEnoughBalls;
    [SerializeField] private TextMeshPro ballCountText;
    private int _ballCount;
    private int _targetBallCount;
    private Timer _timer;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
        SetTimer();
    }

    private void SetTimer()
    {
        _timer.SetTimer(2f);
    }

    private void OnEnable()
    {
        _timer.OnTimeIsUp += OnTimeIsUp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (!_timer.IsTimerRunning())
                _timer.StartTimer();
            else
                _timer.ResetTimer();
            other.tag = "Untagged"; // To prevent count more than one.
            _ballCount++;
            ballCountText.text = _ballCount + "/" + _targetBallCount;
        }
    }

    private void OnTimeIsUp()
    {
        if (_ballCount >= _targetBallCount)
        {
            OnEnoughBalls?.Invoke();
        }
        else
        {
            Debug.Log("YouLose");
            OnNotEnoughBalls?.Invoke();
        }

        _timer.OnTimeIsUp -= OnTimeIsUp;
    }
    
    public void SetTargetBallCount(int count)
    {
        _targetBallCount = count;
    }

    private void OnDisable()
    {
        _timer.OnTimeIsUp -= OnTimeIsUp;
    }
}