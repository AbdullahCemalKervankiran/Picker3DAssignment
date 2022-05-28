
using System;
using UnityEngine;

public class PickerMovement : MonoBehaviour
{
    public event Action OnReachStopPosition;
    [SerializeField] private float border;
    private Transform _stopPosition;
    private Rigidbody _rigidbody;
    private float _positionX, _positionZ;
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

    
    private void FixedUpdate()
    {
        if (Mathf.Abs(_stopPosition.position.z - transform.position.z) >= 0.01f && _isMoved)
        {
            _positionZ += 0.2f;
            _positionX += Input.GetAxis("Horizontal")*0.2f;
            _positionX = Mathf.Clamp(_positionX, -border, border);
            _rigidbody.MovePosition(new Vector3(_positionX,_rigidbody.position.y,_positionZ));
        }
        else if (!_isMoved && Input.GetAxis("Horizontal") != 0f)
        {
            _isMoved = true;
        }
        else if (Mathf.Abs(_stopPosition.position.z - transform.position.z) < 0.01f)
        {
            OnReachStopPosition?.Invoke();
        }
    }

    public void SetStopPosition(Transform t)
    {
        _stopPosition = t;
    }
}
