using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject poolRoad;
    [SerializeField] private PoolCounter poolCounter;

    private void OnEnable()
    {
        poolCounter.OnEnoughBalls += PoolCounterOnOnEnoughBalls;
    }

    private void PoolCounterOnOnEnoughBalls()
    {
        poolRoad.SetActive(true);
    }

    private void OnDisable()
    {
        poolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
    }

    public PoolCounter PoolCounter => poolCounter;
}