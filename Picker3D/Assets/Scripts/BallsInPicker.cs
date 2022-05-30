using DG.Tweening;
using UnityEngine;

public class BallsInPicker : MonoBehaviour
{
    private PickerMovement _pickerMovement;
    private Vector3 _initialPosition;
    private PoolCounter _poolCounter;

    /// <summary>
    ///
    /// This script is used to throw the balls that are inside the picker into the pool when the picker comes to the stop position.
    /// 
    /// </summary>
    private void Awake()
    {
        _pickerMovement = GetComponentInParent<PickerMovement>();
    }

    private void OnEnable()
    {
        _initialPosition = transform.localPosition;
    }

    public void Initialize(PoolCounter poolCounter)
    {
        _poolCounter = poolCounter;
        _poolCounter.OnEnoughBalls += PoolCounterOnOnEnoughBalls;
        _pickerMovement.OnReachStopPosition += PickerMovementOnOnReachStopPosition;
    }

    #region EventHandlers

    private void PoolCounterOnOnEnoughBalls()
    {
        _poolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
        
        //"Balls in picker" returns to initial position if there are enough balls in the pool
        transform.localPosition = _initialPosition; 
    }

    private void PickerMovementOnOnReachStopPosition()
    {
        _pickerMovement.OnReachStopPosition -= PickerMovementOnOnReachStopPosition;
        
        //"Balls in picker" is thrown when reach stop position
        transform.DOMoveZ(transform.position.z + 5, 0.8f);
    }

    #endregion
    
    #region Physics

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) // Ball is put to "Balls in picker"
        {
            other.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball")) // Ball return to module when it go away from "Balls in picker"
        {
            other.transform.parent = GameManager.Instance.CurrentModuleWithPool.Balls.transform;
        }
    }
    
    #endregion
    
    private void OnDisable()
    {
        _pickerMovement.OnReachStopPosition -= PickerMovementOnOnReachStopPosition;
        _poolCounter.OnEnoughBalls -= PoolCounterOnOnEnoughBalls;
    }
}