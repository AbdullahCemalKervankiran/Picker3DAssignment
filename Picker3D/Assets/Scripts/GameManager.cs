using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private ModuleManager _moduleManager;
    private Picker _picker;
    private Module _currentModule;
    private ModuleWithPool _currentModuleWithPool;
    private int _moduleIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _moduleManager = FindObjectOfType<ModuleManager>();
        _picker = FindObjectOfType<Picker>();
    }

    private void OnEnable()
    {
        _moduleManager.LoadModules();
        _moduleIndex = 0;
    }

    private void Start()
    {
        InitializeCurrentModule();
    }

    private void InitializeCurrentModule()
    {
        _currentModule = _moduleManager.GetModule(_moduleIndex);
        _picker.PickerMovement.SetStopPosition(_currentModule.StopPosition.position);
        if (_currentModule.TryGetComponent(out ModuleWithPool moduleWithPool))
        {
            _currentModuleWithPool = moduleWithPool;
           InitializeModuleWithPool();
            
        }
        else
        {
            _picker.PickerMovement.MoveToLastModule();
            _currentModuleWithPool = null;
        }
    }


    private void PoolCounterOnOnEnoughBalls()
    {
        _currentModuleWithPool.PoolManager.PoolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
        _currentModuleWithPool.DeactivateBalls();
        NextModule();
    }

    private void NextModule()
    {
        _moduleIndex++;
        InitializeCurrentModule();
    }

    private void InitializeModuleWithPool()
    {
        _picker.BallsInPicker.SetPoolCounter(_currentModuleWithPool.PoolManager.PoolCounter);
        _picker.BallsInPicker.SubscribeOnEnoughBallsEvent();
        _picker.BallsInPicker.SubscribeOnReachStopPositionEvent();
        _currentModuleWithPool.PoolManager.PoolCounter.OnEnoughBalls += PoolCounterOnOnEnoughBalls;
    }

    private void OnDisable()
    {
        if (_currentModuleWithPool != null)
            _currentModuleWithPool.PoolManager.PoolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
    }

    public Module CurrentModule => _currentModule;

    public ModuleWithPool CurrentModuleWithPool => _currentModuleWithPool;
}