using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerManager : MonoBehaviour
{
    private BallsInPicker _ballsInPicker;
    private PickerMovement _pickerMovement;
    private Module _currentModule;

    private void Awake()
    {
        _ballsInPicker = GetComponentInChildren<BallsInPicker>();
        _pickerMovement = GetComponent<PickerMovement>();
    }

    public void SetPicker(Module module)
    {
        _currentModule = module;
        SetPickerTarget();
        SetPickerPoolCounter();
        PickerSubscribeOnEnoughBallsEvent();
        PickerSubscribeOnReachStopPositionEvent();
    }

    private void SetPickerTarget()
    {
        _pickerMovement.SetStopPosition(_currentModule.StopPosition);
    }

    private void PickerSubscribeOnEnoughBallsEvent()
    {
        _ballsInPicker.SubscribeOnEnoughBallsEvent();
    }
    private void PickerSubscribeOnReachStopPositionEvent()
    {
        _ballsInPicker.SubscribeOnReachStopPositionEvent();
    }

    private void SetPickerPoolCounter()
    {
        _ballsInPicker.SetPoolCounter(_currentModule.PoolManager.PoolCounter);
    }
}