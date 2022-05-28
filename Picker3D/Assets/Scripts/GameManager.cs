using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private ModuleManager _moduleManager;
    private PickerManager _pickerManager;
    private Module _currentModule;
    private int _moduleIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
       
        _moduleManager = FindObjectOfType<ModuleManager>();
        _pickerManager = FindObjectOfType<PickerManager>();
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
        _pickerManager.SetPicker(_currentModule);
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

    private void OnDisable()
    {
        _currentModule.PoolManager.PoolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
    }

    public Module CurrentModule => _currentModule;
}