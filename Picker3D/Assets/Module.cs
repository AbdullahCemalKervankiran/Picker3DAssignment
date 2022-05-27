using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    [SerializeField] private int targetBallCount;
    [SerializeField] private GameObject balls;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private Transform stopPosition;
    public void InitializeModule(Transform t)
    {
        SetLocation(t);
        SetTargetBallCount();
    }
    
    public void DeactivateBalls()
    {
        balls.SetActive(false);
    }
    private void SetLocation(Transform t)
    {
        transform.position = t.position;
    }

    private void SetTargetBallCount()
    {
        poolManager.PoolCounter.SetTargetBallCount(targetBallCount);
    }
    public int TargetBallCount => targetBallCount;

    public PoolManager PoolManager => poolManager;

    public Transform StopPosition => stopPosition;
}
