using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoolCounter : MonoBehaviour
{
    [SerializeField] private TextMeshPro ballCountText;
    private int _ballCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.tag = "Untagged";
            _ballCount++;
            ballCountText.text = _ballCount + "/20";
        }
    }
}
