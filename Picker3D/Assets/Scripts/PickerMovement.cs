using System;
using DG.Tweening;
using UnityEngine;

public class PickerMovement : MonoBehaviour
{
    public event Action OnReachStopPosition;
    [SerializeField] private float border;
    private PowerBar _powerBar;
    private Vector3 _stopPosition;
    private GameObject _pickerLauncher;
    private Rigidbody _rigidbody;
    private float _positionX, _positionZ;
    private bool _isMoved, _locked;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _powerBar = FindObjectOfType<PowerBar>();
        _pickerLauncher = GameObject.FindWithTag("PickerLauncher");
    }

    private void Start()
    {
        RestorePositionZ();
    }

    private void RestorePositionZ()
    {
        _positionZ = _rigidbody.position.z;
    }


    private void FixedUpdate()
    {
        if (Mathf.Abs(_stopPosition.z - transform.position.z) >= 0.01f && _isMoved && !_locked)
        {
            _positionZ += 0.2f;
            _positionX += Input.GetAxis("Horizontal") * 0.2f;
            _positionX = Mathf.Clamp(_positionX, -border, border);
            _rigidbody.MovePosition(new Vector3(_positionX, _rigidbody.position.y, _positionZ));
        }
        else if (!_isMoved && Input.GetAxis("Horizontal") != 0f)
        {
            _isMoved = true;
        }
        else if (Mathf.Abs(_stopPosition.z - transform.position.z) < 0.01f)
        {
            OnReachStopPosition?.Invoke();
        }
    }

    public void SetStopPosition(Vector3 t)
    {
        _stopPosition = t;
        Debug.Log("New Stop Pos: " + _stopPosition);
    }

    public void SetMovementLock(bool b)
    {
        //OnMovingLauncher?.Invoke();
        _locked = b;
        /*Sequence sequence = DOTween.Sequence();
        sequence.Append(_rigidbody.DOMoveX(0, 0.5f));
        sequence.Append(_rigidbody.DOMoveZ(_pickerLauncher.position.z, 5f));*/
    }

    public Rigidbody Rigidbody
    {
        get => _rigidbody;
        set => _rigidbody = value;
    }

    public void MoveToLastModule()
    {
        _locked = true;
        //Sequence sequence = DOTween.Sequence();
        _powerBar.SetSliderOn();

        _rigidbody.DOMoveX(0, 1f).SetUpdate(UpdateType.Fixed).SetEase(Ease.Linear).OnComplete(MoveToLauncher);
    }

    private void MoveToLauncher()
    {
        _rigidbody.DOMoveZ(_pickerLauncher.transform.position.z, 3f).SetUpdate(UpdateType.Fixed)
            .SetEase(Ease.Linear).OnComplete(() =>
                {
                    _powerBar.SetSliderOff();
                    RestorePositionZ();
                    Launch();
                }
            );
    }

    private void Launch()
    {
        _rigidbody
            .DOMoveZ(
                _stopPosition.z - (1f - _powerBar.GetPowerValue()) * (_stopPosition.z - _positionZ),
                2f).SetUpdate(UpdateType.Fixed).SetEase(Ease.InOutSine).OnComplete(LevelCompleted);
    }

    private void LevelCompleted()
    {
       GameManager.Instance.CompleteLevel();
    }
}