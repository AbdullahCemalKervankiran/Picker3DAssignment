using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ModuleManager : MonoBehaviour
{
    [SerializeField] private Transform[] modulePositions;
    [SerializeField] private int[] moduleCounts;
    private List<Module> _modules;

    public void LoadModules()
    {
        _modules = new List<Module>();
        for (int i = 0; i < 4; i++)
        {
            Module m = PickModule(i);
            m.InitializeModule(modulePositions[i]);
            _modules.Add(m);
        }
    }

    private Module PickModule(int id)
    {
        GameObject prefab =
            Instantiate(Resources.Load("Modules/" + GetPathById(id) + "/" + Random.Range(0, moduleCounts[id]))) as
                GameObject;
        if (prefab != null)
            return prefab.GetComponent<Module>();
        return null;
    }
    

    private string GetPathById(int id)
    {
        if (id == 0)
            return "20";
        if (id == 1)
            return "30";
        if (id == 2)
            return "50";
        return "Finish";
    }

    public Module GetModule(int id)
    {
        return _modules[id];
    }
}