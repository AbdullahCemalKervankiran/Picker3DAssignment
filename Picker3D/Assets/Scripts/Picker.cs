using UnityEngine;

public class Picker : MonoBehaviour
{
    private BallsInPicker _ballsInPicker;
    private PickerMovement _pickerMovement;
    
    private void Awake()
    {
        _ballsInPicker = GetComponentInChildren<BallsInPicker>();
        _pickerMovement = GetComponent<PickerMovement>();
    }

    public BallsInPicker BallsInPicker => _ballsInPicker;
    public PickerMovement PickerMovement => _pickerMovement;
}