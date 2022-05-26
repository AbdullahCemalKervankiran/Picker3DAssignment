using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PickerMovement : MonoBehaviour
{
    public Transform stopPoint;
    
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.DOMoveZ(stopPoint.position.z, 5f).SetEase(Ease.Linear).SetUpdate(UpdateType.Fixed); //_rigidbody.velocity += 0.3f*Vector3.forward;
    }
}
