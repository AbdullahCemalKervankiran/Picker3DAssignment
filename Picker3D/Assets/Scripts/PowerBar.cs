using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    [SerializeField] private Slider powerBar;
    private float _power;
    private bool _tookValue;
    
    private void OnEnable()
    {
        powerBar.onValueChanged.AddListener(SetPowerValue);
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
                powerBar.value += 0.075f;
            else if (powerBar.value > 0)
                powerBar.value -= Time.deltaTime * 0.3f;
        }
        
    }

    public float GetPowerValue()
    {
        _tookValue = true;
        return _power;
    }

    public void SetSliderOn()
    {
        powerBar.gameObject.SetActive(true);
    } 
    public void SetSliderOff()
    {
        powerBar.gameObject.SetActive(false);
    } 
}