using UnityEngine;

public class ModuleWithPool : Module
{
    [SerializeField] private GameObject balls;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private ModuleType moduleType;
    
    public void DeactivateBalls()
    {
        balls.SetActive(false);
    }

    public override void InitializeModule(Transform t)
    {
        base.InitializeModule(t);
        SetTargetBallCount();
    }

    private void SetTargetBallCount()
    {
        poolManager.PoolCounter.SetTargetBallCount((int) moduleType);
    }

    private enum ModuleType
    {
        Type20 = 20,
        Type30 = 30,
        Type50 = 50
    }

    public GameObject Balls => balls;
    public PoolManager PoolManager => poolManager;
}