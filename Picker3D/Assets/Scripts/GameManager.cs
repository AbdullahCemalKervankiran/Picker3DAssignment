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

    private void Awake()
    {
        if (Instance != null && Instance != this)
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
            _gameUI.SetSubtitle("Press W! W! W!", 3f);
            _picker.PickerMovement.AlignPicker();
            _currentModuleWithPool = null;
        }
    }


    private void PoolCounterOnOnEnoughBalls()
    {
        _currentModuleWithPool.PoolManager.PoolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
        _currentModuleWithPool.DeactivateBalls();
        _gameUI.SetModuleOK();
        _gameUI.SetSubtitle("Good!", 1f);
        NextModule();
    }

    private void PoolCounterOnOnNotEnoughBalls()
    {
        _gameUI.SetModuleFailed();
    }

    public void CompleteLevel()
    {
        _gameUI.SetLevelCompleted();
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
        _currentModuleWithPool.PoolManager.PoolCounter.OnNotEnoughBalls += PoolCounterOnOnNotEnoughBalls;
    }


    private void OnDisable()
    {
        if (_currentModuleWithPool != null)
        {
            _currentModuleWithPool.PoolManager.PoolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
            _currentModuleWithPool.PoolManager.PoolCounter.OnNotEnoughBalls -= PoolCounterOnOnNotEnoughBalls;
        }
    }

    public Module CurrentModule => _currentModule;

    public GameUI GameUI => _gameUI;
}