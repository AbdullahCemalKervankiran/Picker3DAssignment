using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    
    [SerializeField] private Transform stopPosition;

    public virtual void InitializeModule(Transform t)
    {
        SetLocation(t);
    }

    
    private void SetLocation(Transform t)
    {
        transform.position = t.position;
    }

    public Transform StopPosition => stopPosition;

    
}