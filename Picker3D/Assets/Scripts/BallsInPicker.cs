using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BallsInPicker : MonoBehaviour
{
    private PickerMovement _pickerMovement;
    private Vector3 _previousPosition;
    private PoolCounter _poolCounter;

    private void Awake()
    {
        _pickerMovement = FindObjectOfType<PickerMovement>();
    }

    public void SetPoolCounter(PoolCounter poolCounter)
    {
        _poolCounter = poolCounter;
    }

    public void SubscribeOnEnoughBallsEvent()
    {
        _poolCounter.OnEnoughBalls += PoolCounterOnOnEnoughBalls;
    }

    public void SubscribeOnReachStopPositionEvent()
    {
        _pickerMovement.OnReachStopPosition += PickerMovementOnOnReachStopPosition;
    }

    private void OnEnable()
    {
        _previousPosition = transform.localPosition;
    }

    private void PoolCounterOnOnEnoughBalls()
    {
        _poolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
        transform.localPosition = _previousPosition;
    }

    private void PickerMovementOnOnReachStopPosition()
    {
        _pickerMovement.OnReachStopPosition -= PickerMovementOnOnReachStopPosition;
        transform.DOMoveZ(transform.position.z + 5, 0.5f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.transform.parent = GameManager.Instance.CurrentModule.Balls.transform;
        }
    }
    private void OnDisable()
    {
        _pickerMovement.OnReachStopPosition -= PickerMovementOnOnReachStopPosition;
        _poolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
    }
}