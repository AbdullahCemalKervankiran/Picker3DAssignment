using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PickerMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.DOMoveZ(40, 10).SetUpdate(UpdateType.Fixed); //_rigidbody.velocity += 0.3f*Vector3.forward;
    }
}
