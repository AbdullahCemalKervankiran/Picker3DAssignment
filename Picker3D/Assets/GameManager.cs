using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ModuleManager _moduleManager;
    private PickerMovement _pickerMovement;
    public static GameManager Instance;
    private Module _currentModule;
    private int _moduleIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        _moduleManager = FindObjectOfType<ModuleManager>();
        _pickerMovement = FindObjectOfType<PickerMovement>();
    }

    private void OnEnable()
    {
        _moduleManager.LoadModules();
        _moduleIndex = 0;
        InitializeCurrentModule();
    }

    private void InitializeCurrentModule()
    {
        _currentModule = _moduleManager.GetModule(_moduleIndex);
        SetPickerTarget();
        _currentModule.PoolManager.PoolCounter.OnEnoughBalls += PoolCounterOnOnEnoughBalls;
    }

    private void PoolCounterOnOnEnoughBalls()
    {
        _currentModule.PoolManager.PoolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
        _currentModule.DeactivateBalls();
        NextModule();
    }

    private void NextModule()
    {
        _moduleIndex++;
        InitializeCurrentModule();
    }

    private void SetPickerTarget()
    {
        _pickerMovement.SetStopPosition(_currentModule.StopPosition);
    }
    public Module CurrentModule => _currentModule;
}