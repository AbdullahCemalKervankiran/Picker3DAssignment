using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameUI _gameUI;
    private ModuleManager _moduleManager;
    private Picker _picker;
    private Module _currentModule;
    private ModuleWithPool _currentModuleWithPool;
    private int _moduleIndex;

    /// <summary>
    ///
    /// Game flow:
    /// - Modules are randomly loaded at first
    /// - If there are enough balls in the pool, the next module is unlocked. Otherwise level will be failed.
    /// - If the modules with pool are completed, the last module will be unlocked.
    /// - The level is completed after the score is determined with the last module.
    /// 
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this) // Singleton
            Destroy(this);
        else
            Instance = this;

        _moduleManager = FindObjectOfType<ModuleManager>();
        _gameUI = FindObjectOfType<GameUI>();
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

    #region Module Initialization

    private void InitializeCurrentModule()
    {
        // Get module by module index
        _currentModule = _moduleManager.GetModule(_moduleIndex); 
        
        // Set picker stop position according to current module
        _picker.PickerMovement.SetStopPosition(_currentModule.StopPosition.position);
        
        // If current module has a pool, then "Balls in picker" and Pool events are set
        if (_currentModule.TryGetComponent(out ModuleWithPool moduleWithPool))
        {
            _currentModuleWithPool = moduleWithPool;
            InitializeModuleWithPool();
        }
        else // Otherwise last module is set
        {
            _gameUI.SetSubtitle("Press W! W! W!", 3f);
            _picker.PickerMovement.PrepareForLastModule();
            _currentModuleWithPool = null;
        }
    }

    private void InitializeModuleWithPool()
    {
        _picker.BallsInPicker.Initialize(_currentModuleWithPool.PoolManager.PoolCounter);
        _currentModuleWithPool.PoolManager.PoolCounter.OnEnoughBalls += PoolCounterOnOnEnoughBalls;
        _currentModuleWithPool.PoolManager.PoolCounter.OnNotEnoughBalls += PoolCounterOnOnNotEnoughBalls;
    }

    #endregion
    
    #region Event Handlers

    private void PoolCounterOnOnEnoughBalls() // If there are enough balls in the pool, next module is set
    {
        _currentModuleWithPool.PoolManager.PoolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
        _currentModuleWithPool.DeactivateBalls();
        _gameUI.SetModuleOK();
        _gameUI.SetSubtitle("Good!", 1f);
        NextModule();
    }

    private void PoolCounterOnOnNotEnoughBalls() // If there are not enough balls in the pool, the level is failed
    {
        _gameUI.SetModuleFailed();
    }

    #endregion
    private void NextModule()
    {
        _moduleIndex++;
        InitializeCurrentModule();
    }
    
    public void CompleteLevel()
    {
        _gameUI.SetLevelCompleted();
    }

    private void OnDisable()
    {
        if (_currentModuleWithPool != null)
        {
            _currentModuleWithPool.PoolManager.PoolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
            _currentModuleWithPool.PoolManager.PoolCounter.OnNotEnoughBalls -= PoolCounterOnOnNotEnoughBalls;
        }
    }

    //Getters
    public ModuleWithPool CurrentModuleWithPool => _currentModuleWithPool;

    public GameUI GameUI => _gameUI;
}