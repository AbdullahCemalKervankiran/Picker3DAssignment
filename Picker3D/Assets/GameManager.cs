using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ModuleManager _moduleManager;
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        _moduleManager = FindObjectOfType<ModuleManager>();
    }

    private void Start()
    {
        _moduleManager.LoadModules();
    }
}