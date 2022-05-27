using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject poolRoad;
    [SerializeField] private PoolCounter _poolCounter;

    private void OnEnable()
    {
        _poolCounter.OnEnoughBalls += PoolCounterOnOnEnoughBalls;
    }

    private void PoolCounterOnOnEnoughBalls()
    {
        poolRoad.SetActive(true);
    }

    private void OnDisable()
    {
        _poolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
    }

    public PoolCounter PoolCounter => _poolCounter;
}