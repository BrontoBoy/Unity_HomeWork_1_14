using UnityEngine;
using UnityEngine.Serialization;

public class Coordinator : MonoBehaviour
{
    [SerializeField] private Trigger _trigger;
    [SerializeField] private Alarm _alarm;
    [SerializeField] private Intruder _intruder;
    
    [SerializeField] private bool _isGameActive = true;
    
    
    private void OnEnable()
    {
        if (_trigger != null)
        {
            _trigger.IntruderEntered += HandleIntruderEntered;
            _trigger.IntruderExited += HandleIntruderExited;
        }
    }
    
    private void OnDisable()
    {
        if (_trigger != null)
        {
            _trigger.IntruderEntered -= HandleIntruderEntered;
            _trigger.IntruderExited -= HandleIntruderExited;
        };
    }
    
    private void HandleIntruderEntered()
    {
        if (_isGameActive == false)
        {
            return;
        }
        
        _alarm?.ActivateAlarm();
    }
    
    private void HandleIntruderExited()
    {
        if (_isGameActive == false)
        {
            return;
        }
        
        _alarm?.DeactivateAlarm();
    }
}
