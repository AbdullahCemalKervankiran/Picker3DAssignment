using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    [SerializeField] private int targetBallCount;

    public void SetLocation(Transform t)
    {
        transform.position = t.position;
    }
    
}
