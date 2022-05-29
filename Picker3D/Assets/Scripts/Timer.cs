using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action OnTimeIsUp;
    private float _time;
    private float _elapsedTime;
    private bool _timerRunning;
    private IEnumerator _timerCoroutine;
    
    public void SetTimer(float time)
    {
        if(_timerRunning)
            StopTimer();
        _time = time;
        _elapsedTime = 0f;
        _timerCoroutine = RunTimer();
    }

    public void StartTimer()
    {
        _timerRunning = true;
        StartCoroutine(_timerCoroutine);
    }

    public void ResetTimer()
    {
        _elapsedTime = 0f;
    }

    private void StopTimer()
    {
        _timerRunning = false;
        StopCoroutine(_timerCoroutine);
    }

    public bool IsTimerRunning()
    {
        return _timerRunning;
    }
    
    IEnumerator RunTimer()
    {
        while (_elapsedTime < _time)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        OnTimeIsUp?.Invoke();
        StopTimer();
    }
}
