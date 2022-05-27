using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolRoad : MonoBehaviour
{
    [SerializeField] private PoolCounter poolCounter;
    [SerializeField] private GameObject poolRoad;
    [SerializeField] private GameObject poolBasement;

    private void OnEnable()
    {
        poolCounter.OnEnoughBalls += PoolCounterOnOnEnoughBalls;
    }

    private void PoolCounterOnOnEnoughBalls()
    {
        SetPoolRoad();
    }

    private void SetPoolRoad()
    {
        poolRoad.SetActive(true);
        poolBasement.SetActive(false);
    }

    private void OnDisable()
    {
        poolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
    }
}
