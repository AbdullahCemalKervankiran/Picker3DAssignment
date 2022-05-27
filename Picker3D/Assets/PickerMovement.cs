
using System;
using UnityEngine;

public class PickerMovement : MonoBehaviour
{
    private Transform _stopPosition;
    private Rigidbody _rigidbody;
    private bool _isMoved;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _positionZ = transform.position.z;
    }

    private float _positionX, _positionZ;
    
    private void FixedUpdate()
    {
        if (Mathf.Abs(_stopPosition.position.z - transform.position.z) >= 0.01f && _isMoved)
        {
            _positionZ += 0.2f; 
            _positionX += Input.GetAxis("Horizontal")*0.2f;
            _rigidbody.MovePosition(new Vector3(_positionX,_rigidbody.position.y,_positionZ));
        }
        else if (!_isMoved && Input.GetAxis("Horizontal") != 0f)
        {
            _isMoved = true;
        }
    }

    public void SetStopPosition(Transform t)
    {
        _stopPosition = t;
    }
}
