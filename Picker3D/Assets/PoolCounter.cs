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
    private Timer _timer;

    private void Awake()
    {
        SetTimer();
    }

    private void SetTimer()
    {
        _timer = GetComponent<Timer>();
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
            other.transform.parent = transform;
            other.tag = "Untagged"; // To prevent count more than one.
            _ballCount++;
            ballCountText.text = _ballCount + "/20";
        }
    }

    private void OnTimeIsUp()
    {
        if (_ballCount >= 20)
        {
            Debug.Log("Next Module");
            OnEnoughBalls?.Invoke();
        }
        else
        {
            Debug.Log("YouLose");
            OnNotEnoughBalls?.Invoke();
        }

        _timer.OnTimeIsUp -= OnTimeIsUp;
    }

    private void OnDisable()
    {
        _timer.OnTimeIsUp -= OnTimeIsUp;
    }
}