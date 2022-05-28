using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    private Slider _powerBar;
    private float _power;
    private bool _tookValue;
    private void Awake()
    {
        _powerBar = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _powerBar.onValueChanged.AddListener(SetPowerValue);
    }

    private void SetPowerValue(float value)
    {
        _power = value;
    }

    private void Update()
    {
        if (!_tookValue)
        {
            if (Input.GetKeyDown(KeyCode.W))
                _powerBar.value += 0.075f;
            else if (_powerBar.value > 0)
                _powerBar.value -= Time.deltaTime * 0.3f;
        }
        
    }

    public float GetPowerValue()
    {
        _tookValue = true;
        return _power;
    }
}